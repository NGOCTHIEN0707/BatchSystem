using Domain.ProductionOrders.SnapShot;

namespace BatchSystem.Application.Queries.ProductionOrder.DTO
{
    public class ProductionOrderReportDetail
    {
        public Guid ProductionOrderDetailId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int SequenceNo { get; set; }
        public int TargetBatch { get; set; }
        public int CurrentBatch { get; set; }

        public RecipeSnapshotData? RecipeSnapshot { get; set; }
    }
}
