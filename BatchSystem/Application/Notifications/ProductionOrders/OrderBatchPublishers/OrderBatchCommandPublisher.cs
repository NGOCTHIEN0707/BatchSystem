using BatchSystem.Infrastructure.Communication;
using Domain.OrderBatchs;
using Domain.ProductionOrders;
using Domain.ProductionOrders.SnapShot;
using Microsoft.Extensions.FileSystemGlobbing;
using System.Text.Json;

namespace BatchSystem.Application.Notifications.ProductionOrders.OrderBatchPublishers
{
    public class OrderBatchCommandPublisher : IOrderBatchCommandPublisher
    {
        private readonly IManagedMqttClient _mqttClient;

        public OrderBatchCommandPublisher(IManagedMqttClient mqttClient)
        {
            _mqttClient = mqttClient;
        }

        public async Task PublishBatchGroupReadyAsync(ProductionOrderDetail detail, List<OrderBatch> batches,int productCode,int customerCode ,CancellationToken cancellationToken = default)
        {
            if (detail == null)
                throw new InvalidOperationException("ProductionOrderDetail không tồn tại.");

            if (batches == null || !batches.Any())
                throw new InvalidOperationException("Danh sách OrderBatch rỗng.");

            if (string.IsNullOrWhiteSpace(detail.RecipeSnapshotJson))
                throw new InvalidOperationException("RecipeSnapshotJson không tồn tại.");
            var snapshot = JsonSerializer.Deserialize<RecipeSnapshotData>(detail.RecipeSnapshotJson);
            if (snapshot == null)
                throw new InvalidOperationException("Không thể deserialize RecipeSnapshotJson.");

            var orderedBatches = batches
                .OrderBy(x => x.BatchNo)
                .ToList();
            var firstBatch = orderedBatches.First();
            var payload = new LoadBatchCommandMessage
            {
                Command = "LoadBatch",
                ProductionOrderId = firstBatch.ProductionOrderId.ToString(),
                ProductionOrderDetailId = firstBatch.ProductionOrderDetailId.ToString(),

                StartBatchNo = firstBatch.BatchNo,
                BatchCount = batches.Count,

                Batches = orderedBatches
                    .Select(x => new LoadBatchItemMessage
                    {
                        OrderBatchId = x.OrderBatchId.ToString(),
                        BatchNo = x.BatchNo
                    })
                    .ToList(),

                ProductCode = productCode,
                CustomerCode = customerCode,
                //NumberOfPieces = ResolveNumberOfPieces(orderBatch, snapshot),
                //WeightOfAPiece = ResolveWeightOfAPiece(orderBatch, snapshot),
                GrindingTimeSeconds = snapshot.GrindingTimeSeconds,
                MixingTimeSeconds = snapshot.MixingTimeSeconds,
                Materials = snapshot.Materials
                   .Select((x, index) => new LoadBatchMaterialMessage
                   {
                       MaterialId = MapMaterialId(x),
                       TargetKg = (float)x.TargetKg
                   })
                   .ToList(),

                Timestamp = DateTime.UtcNow
            };

            var topic = BuildTopic(firstBatch);

            var json = JsonSerializer.Serialize(payload);

            if (!_mqttClient.IsConnected)
            {
                await _mqttClient.ConnectAsync(cancellationToken);
            }

            await _mqttClient.PublishAsync(topic, json, retainFlag: false, cancellationToken);
        }
        private static string BuildTopic(OrderBatch orderBatch)
        {
            return "batchsystem/line1/command";
        }
        private static string MapMaterialId(RecipeSnapshotMaterialData material)
        {
            var id = material.MaterialId?.Trim().ToUpperInvariant();

            return id switch
            {
                "MAT_CARROT" => "RAW1",
                "MAT_CHICKEN" => "RAW2",
                "MAT_CORN" => "RAW3",
                "MAT_WATER" => "MAT_WATER",
                "MAT_ADDITIVE" => "MAT_ADDITIVE",

                _ => throw new InvalidOperationException(
                    $"Material không hỗ trợ: {material.MaterialId} - {material.MaterialName}")
            };
        }


    }

}
