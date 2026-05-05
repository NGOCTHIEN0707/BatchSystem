
using Infrastructure;

namespace BatchSystem.Application.Queries.Materials
{
    public class GetAllMaterialsQueryHandler : IRequestHandler<GetAllMaterialsQuery, List<MaterialDto>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllMaterialsQueryHandler(ApplicationDbContext context)
        {
            _context=context;
        }

        public async Task<List<MaterialDto>> Handle(GetAllMaterialsQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Materials
           .AsNoTracking()
           .Select(x => new MaterialDto
           {
               MaterialId = x.MaterialId,
               MaterialName = x.MaterialName
           })
           .ToListAsync(cancellationToken);

                return result;
            }
    }
}
