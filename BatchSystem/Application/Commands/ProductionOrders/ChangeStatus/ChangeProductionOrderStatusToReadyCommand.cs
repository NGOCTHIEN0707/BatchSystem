using Domain.ProductionOrders;
using MediatR;

namespace BatchSystem.Application.Commands.ProductionOrders.ChangeStatus
{
    public class ChangeProductionOrderStatusToReadyCommand : IRequest<bool>
    {
        public Guid ProductionOrderId { get; set; } = Guid.Empty;
        //public ProductionOrderStatus Status { get; set; } = ProductionOrderStatus.Ready;
    }
}
