
using Infrastructure;

namespace BatchSystem.Application.Queries.Recipes
{
    public class GetAllRecipeIdQueryHandler : IRequestHandler<GetAllRecipeIdQuery, List<string>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllRecipeIdQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<string>> Handle(
            GetAllRecipeIdQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Recipes
                .AsNoTracking()
                .Select(x => x.RecipeId)
                .ToListAsync(cancellationToken);
        }
    }
}
