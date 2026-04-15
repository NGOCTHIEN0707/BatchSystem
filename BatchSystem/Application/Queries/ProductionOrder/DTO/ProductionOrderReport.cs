using Domain.ProductionOrders;

namespace BatchSystem.Application.Queries.ProductionOrder.DTO
{
    public class ProductionOrderReport
    {
        public Guid ProductionOrderId { get; set; }
        public string? CustomerName { get; set; }
        public string? Phone { get; set; }
        public DateTime OrderDay { get; set; }
        public ProductionOrderStatus Status {  get; set; } 
        public DateTime? PlannedStartTime { get; set; }
        public DateTime? PlannedEndTime { get; set; }
        public DateTime? ActualStartTime { get; set; }
        public DateTime? ActualEndTime { get; set; }

        public List<ProductionOrderReportDetail> Details { get; set; } = new();
    }
}
