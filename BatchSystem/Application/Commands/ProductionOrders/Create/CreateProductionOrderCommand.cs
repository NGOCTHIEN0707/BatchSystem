using Domain.ProductionOrders;
using MediatR;

namespace BatchSystem.Application.Commands.ProductionOrders.Create
{
    public class CreateProductionOrderCommand : IRequest<bool>
    {
        public int Priority { get; set; }
        public DateTime? PlannedStartTime { get; set; }
        public DateTime? PlannedEndTime { get; set; }
        public string UserName { get; set; } = string.Empty;
        public List<CreateProductionOrderDetailDto> Details { get; set; } = new List<CreateProductionOrderDetailDto>();

        public class CreateProductionOrderDetailDto
        {
            public string ProductId { get; set; } = string.Empty;
            public int BatchQuantity { get; set; }

            public int SequenceNo { get; set; }
        }

        
    }
}
