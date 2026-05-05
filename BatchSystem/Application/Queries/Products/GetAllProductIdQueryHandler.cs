
using BatchSystem.Domain.Products;
using Infrastructure;

namespace BatchSystem.Application.Queries.Products
{
    public class GetAllProductIdQueryHandler : IRequestHandler<GetAllProductIdQuery, List<string>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllProductIdQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<string>> Handle(GetAllProductIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Products
           .AsNoTracking()
           .Select(x => x.ProductId.ToString())
           .ToListAsync(cancellationToken);
        }
    }
}
