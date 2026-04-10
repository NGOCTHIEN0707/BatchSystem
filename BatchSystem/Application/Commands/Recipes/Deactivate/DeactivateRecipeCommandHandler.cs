
using BatchSystem.Domain.Materials;
using BatchSystem.Domain.Recipes;
using BatchSystem.Infrastructure.Repositories;
using Domain.Materials;
using Domain.Recipes;

namespace BatchSystem.Application.Commands.Recipes.Deactivate
{
    public class DeactivateRecipeCommandHandler : IRequestHandler<DeactivateRecipeCommand, bool>
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeactivateRecipeCommandHandler(IRecipeRepository recipeRepository, IUnitOfWork unitOfWork)
        {
            _recipeRepository=recipeRepository;
            _unitOfWork=unitOfWork;
        }

        public async Task<bool> Handle(DeactivateRecipeCommand request, CancellationToken cancellationToken)
        {
            var recipeToDeactivate = await _recipeRepository.GetById(request.RecipeId);
            if (recipeToDeactivate == null) throw new EntityNotFoundException(nameof(Recipe), request.RecipeId);
            if (recipeToDeactivate.IsActive)
            {
                recipeToDeactivate.Deactivate();
            }
            _recipeRepository.UpdateAsync(recipeToDeactivate);
            return await _unitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
