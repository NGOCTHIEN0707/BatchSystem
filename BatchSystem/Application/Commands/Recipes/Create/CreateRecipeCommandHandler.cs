
using BatchSystem.Domain.Materials;
using BatchSystem.Domain.Recipes;
using Domain.Materials;
using Domain.Recipes;

namespace BatchSystem.Application.Commands.Recipes.Create
{
    public class CreateRecipeCommandHandler : IRequestHandler<CreateRecipeCommand, bool>
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateRecipeCommandHandler(IRecipeRepository recipeRepository, IMaterialRepository materialRepository, IUnitOfWork unitOfWork)
        {
            _recipeRepository=recipeRepository;
            _materialRepository=materialRepository;
            _unitOfWork=unitOfWork;
        }

        public async Task<bool> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
        {
            var IsRecipeNameExisted = await _recipeRepository.IsRecipeNameExisted(request.RecipeName);
            if (IsRecipeNameExisted) throw new EntityDuplicationException(nameof(Recipe), request.RecipeName);
            if (request.RecipeMaterials == null || !request.RecipeMaterials.Any())
                throw new BusinessRuleException("Recipe must have at least one material.");
            //var duplicatedMaterialIds = request.RecipeMaterials
            //   .GroupBy(x => x.MaterialId)
            //   .Where(x => x.Count() > 1)
            //   .Select(x => x.Key)
            //   .ToList();
            //if (duplicatedMaterialIds.Any())
            //    throw new BusinessRuleException(
            //        $"Duplicated materials in recipe: {string.Join(", ", duplicatedMaterialIds)}"
            //    );
            foreach (var item in request.RecipeMaterials)
            {
                var material = await _materialRepository.GetById(item.MaterialId);
                if (material == null)
                    throw new EntityNotFoundException(nameof(Material), item.MaterialId);
            }

            var recipe = new Recipe(request.RecipeName, DateTime.Now,request.GrindingTimeSeconds,request.MixingTimeSeconds);

            foreach (var item in request.RecipeMaterials)
            {
                recipe.AddRecipeMaterial(
                    item.MaterialId,
                    item.TargetKg,
                    item.ToleranceMinKg,
                    item.ToleranceMaxKg
                );
            }
            await _recipeRepository.AddAsync(recipe);
            return await _unitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
