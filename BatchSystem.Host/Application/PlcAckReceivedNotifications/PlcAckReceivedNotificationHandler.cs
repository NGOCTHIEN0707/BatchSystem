using BatchSystem.Application.Notifications.ProductionOrders.OrderBatchPublishers;
using BatchSystem.Application.ProductionOrderDispatchers;
using BatchSystem.Domain.OrderBatchs;
using BatchSystem.Domain.OrderBatchs.OrderBatchStatusHistories;
using BatchSystem.Domain.ProductionOrders;
using BatchSystem.Domain.ProductionOrders.ProductionOrderStatusHistories;
using BatchSystem.Domain.SeedWork;
using Domain.OrderBatchs;
using Domain.OrderBatchs.BatchWeightResults;
using Domain.ProductionOrders;
using MediatR;

namespace BatchSystem.Host.Application.PlcAckReceivedNotifications
{
    public class PlcAckReceivedNotificationHandler : INotificationHandler<PlcAckReceivedNotification>
    {
        private readonly IOrderBatchRepository _orderBatchRepository;
        private readonly IOrderBatchStatusHistoryRepository _orderBatchStatusHistoryRepository;
        private readonly IProductionOrderRepository _productionOrderRepository;
        private readonly IOrderBatchCommandPublisher _orderBatchCommandPublisher;
        private readonly IProductionOrderStatusHistoryRepository _productionOrderStatusHistoryRepository;
        private readonly IProductionOrderDispatcher _dispatcher;
        private readonly IUnitOfWork _unitOfWork;

        public PlcAckReceivedNotificationHandler(IOrderBatchRepository orderBatchRepository, IOrderBatchStatusHistoryRepository orderBatchStatusHistoryRepository, IProductionOrderRepository productionOrderRepository, IOrderBatchCommandPublisher orderBatchCommandPublisher, IProductionOrderStatusHistoryRepository productionOrderStatusHistoryRepository, IProductionOrderDispatcher dispatcher, IUnitOfWork unitOfWork)
        {
            _orderBatchRepository=orderBatchRepository;
            _orderBatchStatusHistoryRepository=orderBatchStatusHistoryRepository;
            _productionOrderRepository=productionOrderRepository;
            _orderBatchCommandPublisher=orderBatchCommandPublisher;
            _productionOrderStatusHistoryRepository=productionOrderStatusHistoryRepository;
            _dispatcher=dispatcher;
            _unitOfWork=unitOfWork;
        }

        public async Task Handle(PlcAckReceivedNotification notification, CancellationToken cancellationToken)
        {
            
            var ack = notification.AckMessage;
            Console.WriteLine($"[HOST ACK] OrderBatchId={ack.OrderBatchId}, Status={ack.Status}");
            var batch = await _orderBatchRepository.GetById(ack.OrderBatchId);
            Console.WriteLine($"[HOST ACK] BatchFound={batch != null}, CurrentStatus={batch?.Status}");
            if (batch == null) return;

            var newStatus = MapStatus(ack.Status);
            if (batch.Status == newStatus)
                return;
            var history = batch.ChangeStatus(newStatus);

            await _orderBatchStatusHistoryRepository.AddAsync(history);
            _orderBatchRepository.UpdateAsync(batch);

            if (newStatus == OrderBatchStatus.Completed)
            {
                await _unitOfWork.SaveEntitiesAsync(cancellationToken);
                await HandleBatchCompleted(batch, ack, cancellationToken);
                
                return;
            }

            switch (newStatus)
            {
                case OrderBatchStatus.Received:
                    // chỉ log, chưa cần làm gì thêm    
                    Console.WriteLine("Command đã được PLC Received");
                    break;

                case OrderBatchStatus.Running:
                    var order = await _productionOrderRepository.GetByIdTracking(batch.ProductionOrderId);
                    if (order != null && order.Status != ProductionOrderStatus.Running)
                    {
                        order.UpdateProductionOrderActualStartTime(DateTime.UtcNow);
                        var orderHistory = order.ChangeStatus(ProductionOrderStatus.Running);
                        await _productionOrderStatusHistoryRepository.AddAsync(orderHistory);
                    }
                    break;
                case OrderBatchStatus.WeighingCompleted:
                    SaveBatchWeighingResults(batch, ack);
                    break;
                case OrderBatchStatus.Error:
                    // xử lý lỗi (log, notify, etc.)
                    break;
            }
            await _unitOfWork.SaveEntitiesAsync(cancellationToken);
            Console.WriteLine($"[HOST ACK SAVED] OrderBatchId={ack.OrderBatchId}, NewStatus={newStatus}");
        }
        private async Task HandleBatchCompleted(OrderBatch batch, MqttAckMessage ack, CancellationToken cancellationToken)
        {
            _orderBatchRepository.UpdateAsync(batch);
            
            var productionOrder = await _productionOrderRepository.GetByIdTracking(batch.ProductionOrderId);

            if (productionOrder == null)
                return;

            var currentDetail = productionOrder.ProductionOrderDetails
                .FirstOrDefault(d => d.ProductionOrderDetailId == batch.ProductionOrderDetailId);

            if (currentDetail == null)
                return;

            var currentGroupDone = currentDetail.OrderBatches
                .All(b => b.Status == OrderBatchStatus.Completed);

            if (!currentGroupDone)
                return;

            var nextGroup = productionOrder.ProductionOrderDetails
                .OrderBy(d => d.SequenceNo)
                .Select(d => new
                {
                    Detail = d,
                    Batches = d.OrderBatches
                        .Where(b => b.Status == OrderBatchStatus.Pending)
                        .OrderBy(b => b.BatchNo)
                        .ToList()
                })
                .FirstOrDefault(x => x.Batches.Any());

            if (nextGroup != null)
            {
                foreach (var nextBatch in nextGroup.Batches)
                {
                    var sentHistory = nextBatch.ChangeStatus(OrderBatchStatus.Sent);
                    await _orderBatchStatusHistoryRepository.AddAsync(sentHistory);
                    try
                    {
                        Console.WriteLine($"NextBatchId: {nextBatch.OrderBatchId}");
                        Console.WriteLine($"State before update: {nextBatch.Status}");
                        //_orderBatchRepository.UpdateAsync(nextBatch);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ERROR UPDATE NEXT BATCH: " + ex.Message);
                        Console.WriteLine(ex.StackTrace);
                        throw;
                    }
                }

                await _unitOfWork.SaveEntitiesAsync(cancellationToken);
                await _orderBatchCommandPublisher.PublishBatchGroupReadyAsync(
                    nextGroup.Detail,
                    nextGroup.Batches,
                    1, 2,
                    cancellationToken);

                return;
            }

            Console.WriteLine($"All batches completed for ProductionOrder {productionOrder.ProductionOrderId}");

            if (productionOrder.Status != ProductionOrderStatus.Completed)
            {
                productionOrder.UpdateProductionOrderActualEndTime(DateTime.UtcNow);
                var orderHistory = productionOrder.ChangeStatus(ProductionOrderStatus.Completed);
                await _productionOrderStatusHistoryRepository.AddAsync(orderHistory);
                await _unitOfWork.SaveEntitiesAsync(cancellationToken);
            }
            await _dispatcher.TryDispatchNextAsync(cancellationToken);

        }

        private void SaveBatchWeighingResults(OrderBatch batch, MqttAckMessage ack)
        {
            var snapshot = batch.ProductionOrderDetail.GetRecipeSnapshot();
            if (snapshot == null)
                throw new Exception("RecipeSnapshotJson không tồn tại.");
            if (batch.BatchWeighingResults.Any())
            {
                Console.WriteLine($"[SKIP] WeighingResults already saved for batch {batch.OrderBatchId}");
                return;
            }
            if (ack.WeighingResults == null || !ack.WeighingResults.Any())
                throw new Exception("WeighingCompleted ACK không có WeighingResults.");

            foreach (var item in ack.WeighingResults)
            {

                if (!PlcRawToMaterialId.TryGetValue(item.MaterialId, out var realMaterialId))
                    throw new Exception($"Không map được material {item.MaterialId}.");

                var snapshotMaterial = snapshot.Materials
                    .FirstOrDefault(x => x.MaterialId == realMaterialId);

                if (snapshotMaterial == null)
                    throw new Exception($"Không tìm thấy material {realMaterialId} trong snapshot.");

                var result = new BatchWeighingResult(
                    batch.OrderBatchId,
                    realMaterialId,
                    item.TargetKg,
                    item.ActualKg,
                    (float)snapshotMaterial.ToleranceMinKg,
                    (float)snapshotMaterial.ToleranceMaxKg
                );

                batch.AddWeighingResult(result);
            }
        }

       
        private OrderBatchStatus MapStatus(string status)
        {
            return status switch
            {
                "Received" => OrderBatchStatus.Received,
                "Running" => OrderBatchStatus.Running,
                "WeighingCompleted" => OrderBatchStatus.WeighingCompleted,
                "Completed" => OrderBatchStatus.Completed,
                "Error" => OrderBatchStatus.Error,
                _ => throw new Exception($"Unknown status: {status}")
            };
        }
        private static readonly Dictionary<string, string> PlcRawToMaterialId = new()
        {
            { "RAW1", "MAT_CARROT" },
            { "RAW2", "MAT_CHICKEN" },
            { "RAW3", "MAT_CORN" },
            { "MAT_WATER", "MAT_WATER" },
            { "MAT_ADDITIVE", "MAT_ADDITIVE" }
        };
    }
}
