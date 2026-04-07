using Domain.ProductionOrders;
using MediatR;

namespace BatchSystem.Application.Commands.ProductionOrders
{
    public class CreateProductionOrderCommand : IRequest<bool>
    {
        public int Priority { get; set; }
        public DateTime? PlannedStartTime { get; set; }
        public DateTime? PlannedEndTime { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public List<CreateProductionOrderDetailDto> Details { get; set; } = new List<CreateProductionOrderDetailDto>();

        public class CreateProductionOrderDetailDto
        {
            public string ProductId { get; set; } = string.Empty;
            public int BatchQuantity { get; set; }

            public int SequenceNo { get; set; }
        }

        public CreateProductionOrderCommand(int priority, DateTime? plannedStartTime, DateTime? plannedEndTime, string createdBy, List<CreateProductionOrderDetailDto> details)
        {
            Priority=priority;
            PlannedStartTime=plannedStartTime;
            PlannedEndTime=plannedEndTime;
            CreatedBy=createdBy;
            Details=details;
        }
    }
}
