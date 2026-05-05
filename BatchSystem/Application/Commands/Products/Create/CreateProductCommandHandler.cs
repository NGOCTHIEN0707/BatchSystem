
using BatchSystem.Domain.Products;
using BatchSystem.Domain.Recipes;
using Domain.Products;
using Domain.Recipes;

namespace BatchSystem.Application.Commands.Products.Create
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, bool>
    {
        private readonly IProductRepository _productRepostiory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRecipeRepository _recipeRepository;

        public CreateProductCommandHandler(IProductRepository productRepostiory, IUnitOfWork unitOfWork, IRecipeRepository recipeRepository)
        {
            _productRepostiory=productRepostiory;
            _unitOfWork=unitOfWork;
            _recipeRepository=recipeRepository;
        }

        public async Task<bool> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            if (request.ProductName == null) throw new Exception("Invalid Data to create Product");
            var recipeCheck = await _recipeRepository.GetById(request.RecipeId);
            if (recipeCheck == null) throw new EntityNotFoundException(nameof(Recipe), request.RecipeId);
            var isProductNameExisted = await _productRepostiory.IsProductNameExisted(request.ProductName);
            if (isProductNameExisted) throw new EntityDuplicationException(nameof(Product), request.ProductName);

            var productToCreate = new Product(request.ProductName, request.RecipeId, request.weightPerPieceKg);
            await _productRepostiory.AddAsync(productToCreate);
            return await _unitOfWork.SaveEntitiesAsync(cancellationToken);



        }
    }
}
