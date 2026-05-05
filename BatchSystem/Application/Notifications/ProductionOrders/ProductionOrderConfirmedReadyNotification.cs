namespace BatchSystem.Application.Notifications.ProductionOrders
{
    public class ProductionOrderConfirmedReadyNotification : INotification
    {
        public Guid ProductionOrderId { get; }
        public Dictionary<Guid, int> DetailProductCodes { get; }
        public int CustomerCode { get; }

        public ProductionOrderConfirmedReadyNotification(Guid productionOrderId, Dictionary<Guid, int> detailProductCodes, int customerCode)
        {
            ProductionOrderId=productionOrderId;
            DetailProductCodes=detailProductCodes;
            CustomerCode=customerCode;
        }
    }
}
