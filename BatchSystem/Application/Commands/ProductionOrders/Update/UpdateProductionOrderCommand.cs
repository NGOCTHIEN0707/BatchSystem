using MediatR;

namespace BatchSystem.Application.Commands.ProductionOrders.Update
{
    public class UpdateProductionOrderCommand : IRequest<bool>
    {
        public Guid ProductionOrderId { get; set; }
        public int? Priority { get; set; } = null;
        public DateTime? PlannedStartTime { get; set; }
        public DateTime? PlannedEndTime { get; set; }
        //public string CreatedBy { get; set; } = string.Empty;

        public List<UpdateProductionOrderDetailDto>? Details { get; set; }

        public class UpdateProductionOrderDetailDto
        {
            public Guid ProductionOrderDetailId { get; set; } = Guid.Empty;
            public string ProductId { get; set; } = string.Empty;
            public int BatchQuantity { get; set; }
            public int SequenceNo { get; set; }
        }
    }
}
