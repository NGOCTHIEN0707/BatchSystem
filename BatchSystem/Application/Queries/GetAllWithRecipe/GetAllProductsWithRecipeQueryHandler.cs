using BatchSystem.Application.Queries.Product_Recipe;
using Infrastructure;

namespace BatchSystem.Application.Queries.GetAllWithRecipe
{
    public class GetAllProductsWithRecipeQueryHandler : IRequestHandler<GetAllProductsWithRecipeQuery, List<ProductWithRecipeDto>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllProductsWithRecipeQueryHandler(ApplicationDbContext context)
        {
            _context=context;
        }

        public async Task<List<ProductWithRecipeDto>> Handle(GetAllProductsWithRecipeQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Products
                .AsNoTracking()
                .Select(p => new ProductWithRecipeDto
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Recipe = p.Recipe == null ? null : new RecipeDto
                    {
                        RecipeId = p.RecipeId,
                        RecipeName = p.Recipe.RecipeName,
                        Materials = p.Recipe.RecipeMaterials.Select(rm => new RecipeMaterialDto
                        {
                            MaterialId = rm.MaterialId,
                            MaterialName = rm.Material != null ? rm.Material.MaterialName : "",
                            TargetKg = rm.TargetKg,
                            ToleranceMinKg = rm.ToleranceMinKg,
                            ToleranceMaxKg = rm.ToleranceMaxKg
                        }).ToList()
                    }
                })
                .ToListAsync(cancellationToken);


            return result;
        }
    }
}
