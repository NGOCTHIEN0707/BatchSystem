using Domain.OrderBatchs;
using Domain.ProductionOrders;

namespace BatchSystem.Application.Notifications.ProductionOrders.OrderBatchPublishers
{
    public interface IOrderBatchCommandPublisher
    {
        Task PublishBatchGroupReadyAsync(ProductionOrderDetail detail, List<OrderBatch> batches,int productCode, int customerCode, CancellationToken cancellationToken = default);
    }
}
