using BatchSystem.Application.Queries.ProductionOrder.DTO;

namespace BatchSystem.Application.Queries.ProductionOrder
{
    public class ProductionOrderQuery : IRequest<IEnumerable<ProductionOrderReport>>
    {
        public string UserName { get; set; }
    }
}
