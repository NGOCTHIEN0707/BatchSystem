using BatchSystem.Infrastructure.Communication;
using Domain.OrderBatchs;
using Domain.ProductionOrders.SnapShot;
using System.Text.Json;

namespace BatchSystem.Application.Notifications.ProductionOrders.OrderBatchPublishers
{
    public class OrderBatchCommandPublisher : IOrderBatchCommandPublisher
    {
        private readonly IManagedMqttClient _mqttClient;

        public OrderBatchCommandPublisher(IManagedMqttClient mqttClient)
        {
            _mqttClient=mqttClient;
        }

        public async Task PublishBatchReadyAsync(OrderBatch orderBatch, CancellationToken cancellationToken = default)
        {
            if (orderBatch.ProductionOrderDetail == null)
                throw new InvalidOperationException("OrderBatch chưa có ProductionOrderDetail.");

            if (string.IsNullOrWhiteSpace(orderBatch.ProductionOrderDetail.RecipeSnapshotJson))
                throw new InvalidOperationException("RecipeSnapshotJson không tồn tại.");

            var snapshot = JsonSerializer.Deserialize<RecipeSnapshotData>(
                orderBatch.ProductionOrderDetail.RecipeSnapshotJson);

            if (snapshot == null)
                throw new InvalidOperationException("Không thể deserialize RecipeSnapshotJson.");

            var payload = new
            {
                command = "LoadBatch",
                orderBatchId = orderBatch.OrderBatchId,
                productionOrderId = orderBatch.ProductionOrderId,
                productionOrderDetailId = orderBatch.ProductionOrderDetailId,
                batchNo = orderBatch.BatchNo,
                lineId = orderBatch.LineId,
                productId = snapshot.ProductId,
                productName = snapshot.ProductName,
                recipeId = snapshot.RecipeId,
                recipeName = snapshot.RecipeName,
                materials = snapshot.Materials.Select(x => new
                {
                    materialId = x.MaterialId,
                    materialName = x.MaterialName,
                    targetKg = x.TargetKg,
                    toleranceMinKg = x.ToleranceMinKg,
                    toleranceMaxKg = x.ToleranceMaxKg
                }),
                timestamp = DateTime.Now
            };

            var topic = BuildTopic(orderBatch);

            var json = JsonSerializer.Serialize(payload);

            if (!_mqttClient.IsConnected)
            {
                await _mqttClient.ConnectAsync(cancellationToken);
            }

            await _mqttClient.PublishAsync(topic, json, retainFlag: false, cancellationToken);
        }
        private static string BuildTopic(OrderBatch orderBatch)
        {
            if (!string.IsNullOrWhiteSpace(orderBatch.LineId))
            {
                return $"factory/line/{orderBatch.LineId}/commands/load-batch";
            }

            return "factory/commands/load-batch";
        }
    }

}
