
using BatchSystem.Application.Notifications.ProductionOrders.OrderBatchPublishers;
using BatchSystem.Domain.OrderBatchs;
using BatchSystem.Domain.ProductionOrders;

namespace BatchSystem.Application.Notifications.ProductionOrders
{
    public class ProductionOrderConfirmedReadyNotificationHandler : INotificationHandler<ProductionOrderConfirmedReadyNotification>
    {
        private readonly IProductionOrderRepository _productionOrderRepository;
        private readonly IOrderBatchRepository _orderBatchRepository;
        private readonly IOrderBatchCommandPublisher _orderBatchCommandPublisher;
        private readonly ILogger<ProductionOrderConfirmedReadyNotificationHandler> _logger;

        public ProductionOrderConfirmedReadyNotificationHandler(IProductionOrderRepository productionOrderRepository, IOrderBatchRepository orderBatchRepository, IOrderBatchCommandPublisher orderBatchCommandPublisher, ILogger<ProductionOrderConfirmedReadyNotificationHandler> logger)
        {
            _productionOrderRepository=productionOrderRepository;
            _orderBatchRepository=orderBatchRepository;
            _orderBatchCommandPublisher=orderBatchCommandPublisher;
            _logger=logger;
        }

        public async Task Handle(ProductionOrderConfirmedReadyNotification notification, CancellationToken cancellationToken)
        {
            var productionOrder = await _productionOrderRepository
                .GetById(notification.ProductionOrderId);
            if (productionOrder == null)
            {
                _logger.LogWarning("Không tìm thấy ProductionOrder {Id}", notification.ProductionOrderId);
                return;
            }
            var batches = productionOrder
                .ProductionOrderDetails
                .SelectMany(d => d.OrderBatches)
                .ToList();
            foreach (var Batch in batches)
            {
                var orderBatch = await _orderBatchRepository.GetById(Batch.OrderBatchId);

                if (orderBatch == null)
                {
                    _logger.LogWarning("Không tìm thấy OrderBatch {OrderBatchId} để publish MQTT.", Batch);
                    continue;
                }

                await _orderBatchCommandPublisher.PublishBatchReadyAsync(orderBatch, cancellationToken);
            }
            foreach (var batch in batches)
            {
                try
                {
                    await _orderBatchCommandPublisher.PublishBatchReadyAsync(batch, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Publish batch {BatchId} thất bại", batch.OrderBatchId);
                }
            }
        }
    }
}
