
using BatchSystem.Domain.Materials;
using BatchSystem.Domain.Recipes;
using Domain.Materials;
using Domain.Recipes;

namespace BatchSystem.Application.Commands.Recipes.Update
{
    public class UpdateRecipeCommandHandler : IRequestHandler<UpdateRecipeCommand, bool>
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRecipeCommandHandler(IRecipeRepository recipeRepository, IMaterialRepository materialRepository, IUnitOfWork unitOfWork)
        {
            _recipeRepository=recipeRepository;
            _materialRepository=materialRepository;
            _unitOfWork=unitOfWork;
        }

        public async Task<bool> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
        {
            var recipeToUpdate = await _recipeRepository.GetById(request.RecipeId);
            if (recipeToUpdate == null) throw new EntityNotFoundException(nameof(Recipe), request.RecipeId);

            var hasNameUpdate = !string.IsNullOrWhiteSpace(request.RecipeName);
            var hasMaterialUpdate = request.RecipeMaterials != null;

            if (request.GrindingTimeSeconds != null)
            {
                recipeToUpdate.UpdateGrindingTimeSeconds(request.GrindingTimeSeconds.Value);
            }
            if (request.MixingTimeSeconds !=null)
            {
                recipeToUpdate.UpdateMixingTimeSeconds(request.MixingTimeSeconds.Value);
            }
            if (!hasNameUpdate && !hasMaterialUpdate)
                throw new BusinessRuleException("Nothing to update.");
            if (hasNameUpdate)
            {
                var isRecipeNameExisted = await _recipeRepository.IsRecipeNameExisted(request.RecipeName!);
                if (isRecipeNameExisted && recipeToUpdate.RecipeName != request.RecipeName)
                    throw new EntityDuplicationException(nameof(Recipe), request.RecipeName!);

                recipeToUpdate.UpdateRecipeName(request.RecipeName!);
            }
            if (hasMaterialUpdate)
            {
                var recipeMaterials = request.RecipeMaterials!;
                foreach (var item in recipeMaterials)
                {
                    var material = await _materialRepository.GetById(item.MaterialId);
                    if (material == null)
                        throw new EntityNotFoundException(nameof(Material), item.MaterialId);
                }
                var inputs = recipeMaterials
                .Select(x => new RecipeMaterialInput
                {
                    MaterialId = x.MaterialId,
                    TargetKg = x.TargetKg,
                    ToleranceMinKg = x.ToleranceMinKg,
                    ToleranceMaxKg = x.ToleranceMaxKg
                })
                .ToList();

                recipeToUpdate.SyncRecipeMaterials(inputs);
            }
            _recipeRepository.UpdateAsync(recipeToUpdate);
            return await _unitOfWork.SaveEntitiesAsync(cancellationToken);

        }
    }
}
