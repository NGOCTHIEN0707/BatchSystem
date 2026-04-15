using MediatR;

namespace BatchSystem.Application.Commands.ProductionOrders.Delete
{
    public class DeleteProductionOrderCommand : IRequest<bool>
    {
        public string ProductionOrderId { get; set; } = string.Empty;
    }
}
