using Domain.OrderBatchs;

namespace BatchSystem.Application.Notifications.ProductionOrders.OrderBatchPublishers
{
    public interface IOrderBatchCommandPublisher
    {
        Task PublishBatchReadyAsync(OrderBatch orderBatch, CancellationToken cancellationToken = default);
    }
}
