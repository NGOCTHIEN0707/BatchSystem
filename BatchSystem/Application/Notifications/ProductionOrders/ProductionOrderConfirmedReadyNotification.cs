namespace BatchSystem.Application.Notifications.ProductionOrders
{
    public class ProductionOrderConfirmedReadyNotification : INotification
    {
        public Guid ProductionOrderId { get; }

        public ProductionOrderConfirmedReadyNotification(Guid productionOrderId)
        {
            ProductionOrderId=productionOrderId;
        }
    }
}
