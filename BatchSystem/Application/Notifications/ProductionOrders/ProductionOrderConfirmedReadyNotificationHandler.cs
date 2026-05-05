
using BatchSystem.Application.Notifications.ProductionOrders.OrderBatchPublishers;
using BatchSystem.Domain.OrderBatchs;
using BatchSystem.Domain.OrderBatchs.OrderBatchStatusHistories;
using BatchSystem.Domain.ProductionOrders;
using BatchSystem.Infrastructure.Repositories;
using Domain.OrderBatchs;

namespace BatchSystem.Application.Notifications.ProductionOrders
{
    public class ProductionOrderConfirmedReadyNotificationHandler : INotificationHandler<ProductionOrderConfirmedReadyNotification>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductionOrderRepository _productionOrderRepository;
        private readonly IOrderBatchRepository _orderBatchRepository;
        private readonly IOrderBatchCommandPublisher _orderBatchCommandPublisher;
        private readonly IOrderBatchStatusHistoryRepository _orderBatchStatusHistoryRepository;
        private readonly ILogger<ProductionOrderConfirmedReadyNotificationHandler> _logger;

        public ProductionOrderConfirmedReadyNotificationHandler(IUnitOfWork unitOfWork, IProductionOrderRepository productionOrderRepository, IOrderBatchRepository orderBatchRepository, IOrderBatchCommandPublisher orderBatchCommandPublisher, IOrderBatchStatusHistoryRepository orderBatchStatusHistoryRepository, ILogger<ProductionOrderConfirmedReadyNotificationHandler> logger)
        {
            _unitOfWork=unitOfWork;
            _productionOrderRepository=productionOrderRepository;
            _orderBatchRepository=orderBatchRepository;
            _orderBatchCommandPublisher=orderBatchCommandPublisher;
            _orderBatchStatusHistoryRepository=orderBatchStatusHistoryRepository;
            _logger=logger;
        }

        public async Task Handle(ProductionOrderConfirmedReadyNotification notification, CancellationToken cancellationToken)
        {
            var productionOrder = await _productionOrderRepository
                .GetByIdTracking(notification.ProductionOrderId);
            if (productionOrder == null)
            {
                _logger.LogWarning("Không tìm thấy ProductionOrder {Id}", notification.ProductionOrderId);
                return;
            }

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


            if (nextGroup == null)
            {
                _logger.LogWarning(
                       "ProductionOrder {Id} không có group OrderBatch nào để publish.",
                       notification.ProductionOrderId);

                return;
            }
            try
            {
                if (!notification.DetailProductCodes.TryGetValue(
                   nextGroup.Detail.ProductionOrderDetailId,
                   out var productCode))
                {
                    _logger.LogWarning(
                        "Không tìm thấy ProductCode cho ProductionOrderDetailId={DetailId}",
                        nextGroup.Detail.ProductionOrderDetailId);

                    return;
                }
                foreach (var batch in nextGroup.Batches)
                {
                    var history = batch.ChangeStatus(OrderBatchStatus.Sent);
                    await _orderBatchStatusHistoryRepository.AddAsync(history);
                }
                await _unitOfWork.SaveEntitiesAsync(cancellationToken);//Sent
                await _orderBatchCommandPublisher.PublishBatchGroupReadyAsync(
                    nextGroup.Detail,
                    nextGroup.Batches,
                    productCode,
                    notification.CustomerCode,
                    cancellationToken);

                _logger.LogInformation(
                    "Đã publish batch group đầu tiên. ProductionOrderDetailId={DetailId}, StartBatchNo={StartBatchNo}, BatchCount={BatchCount}",
                    nextGroup.Detail.ProductionOrderDetailId,
                    nextGroup.Batches.First().BatchNo,
                    nextGroup.Batches.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Publish batch group thất bại. ProductionOrderDetailId={DetailId}",
                    nextGroup.Detail.ProductionOrderDetailId);
            }

        }
    }
}
