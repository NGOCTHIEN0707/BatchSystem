
using AutoMapper;
using BatchSystem.Application.Queries.ProductionOrder.DTO;
using Domain.Logins;
using Infrastructure;

namespace BatchSystem.Application.Queries.ProductionOrder
{
    public class ProductionOrderQueryHandler : IRequestHandler<ProductionOrderQuery, IEnumerable<ProductionOrderReport>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductionOrderQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context=context;
            _mapper=mapper;
        }

        public async Task<IEnumerable<ProductionOrderReport>> Handle(ProductionOrderQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.ProductionOrders
            .Include(x => x.CustomerLogin)
            .Include(x => x.ProductionOrderDetails)
                .ThenInclude(d => d.Product)
            .Include(x => x.ProductionOrderDetails)
                .ThenInclude(d => d.OrderBatches)
            .AsNoTracking()
            .Where(x => x.CustomerLogin != null && x.CustomerLogin.UserName == request.UserName);
            var productionOrderByUserName = await queryable.ToListAsync(cancellationToken);
            if (!productionOrderByUserName.Any())
                throw new EntityNotFoundException(nameof(Login), request.UserName);
            var productionOrderRepot = _mapper.Map<IEnumerable<ProductionOrderReport>>(productionOrderByUserName);
            return productionOrderRepot;
        }
    }
}
