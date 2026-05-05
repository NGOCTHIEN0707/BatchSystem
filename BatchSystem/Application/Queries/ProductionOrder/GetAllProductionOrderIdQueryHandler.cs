
using Infrastructure;

namespace BatchSystem.Application.Queries.ProductionOrder
{
    public class GetAllProductionOrderIdQueryHandler : IRequestHandler<GetAllProductionOrderIdQuery, List<Guid>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllProductionOrderIdQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Guid>> Handle(
            GetAllProductionOrderIdQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.ProductionOrders
                .AsNoTracking()
                .Select(x => x.ProductionOrderId)
                .ToListAsync(cancellationToken);
        }
    }
}
