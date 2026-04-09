using BatchSystem.Application.Commands.Prpoducts.Update;
using BatchSystem.Domain.Products;
using BatchSystem.Domain.Recipes;
using Domain.Products;
using Domain.Recipes;

namespace BatchSystem.Application.Commands.Products.Update
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository _productRepostiory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRecipeRepository _recipeRepository;

        public UpdateProductCommandHandler(IProductRepository productRepostiory, IUnitOfWork unitOfWork, IRecipeRepository recipeRepository)
        {
            _productRepostiory=productRepostiory;
            _unitOfWork=unitOfWork;
            _recipeRepository=recipeRepository;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productToUpdate = await _productRepostiory.GetById(request.ProductId);
            if (productToUpdate == null) throw new EntityNotFoundException(nameof(Product),request.ProductId);
            var recipeCheck = await _recipeRepository.GetById(request.RecipeId);
            if (recipeCheck == null) throw new EntityNotFoundException(nameof(Recipe), request.RecipeId);
            if(request.ProductName != null) productToUpdate.UpdateProductName(request.ProductName);
            if(request.RecipeId != null) productToUpdate.UpdateRecipeId(request.RecipeId);

            _productRepostiory.UpdateAsync(productToUpdate);
            return await _unitOfWork.SaveEntitiesAsync(cancellationToken);
            
        }
    }
}
