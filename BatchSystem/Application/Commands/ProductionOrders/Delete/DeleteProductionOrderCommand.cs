using MediatR;

namespace BatchSystem.Application.Commands.ProductionOrders.Delete
{
    public class DeleteProductionOrderCommand : IRequest<bool>
    {
        public Guid ProductionOrderId { get; set; } = Guid.Empty;
    }
}
