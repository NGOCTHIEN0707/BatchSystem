using BatchSystem.Domain.Logins;
using BatchSystem.Domain.ProductionOrders;
using BatchSystem.Domain.Products;
using BatchSystem.Domain.Recipes;
using BatchSystem.Domain.SeedWork;
using Domain.Logins;
using Domain.ProductionOrders;
using Domain.ProductionOrders.SnapShot;
using Domain.Products;
using Domain.Recipes;
using MediatR;

namespace BatchSystem.Application.Commands.ProductionOrders.Create
{
    public class CreateProductionOrderCommandHandler : IRequestHandler<CreateProductionOrderCommand, bool>
    {
        private readonly IProductionOrderRepository _productionOrderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoginRepository _loginRepository;

        public CreateProductionOrderCommandHandler(IProductionOrderRepository productionOrderRepository, IProductRepository productRepository, IRecipeRepository recipeRepository, IUnitOfWork unitOfWork, ILoginRepository loginRepository)
        {
            _productionOrderRepository=productionOrderRepository;
            _productRepository=productRepository;
            _recipeRepository=recipeRepository;
            _unitOfWork=unitOfWork;
            _loginRepository=loginRepository;
        }

        public async Task<bool> Handle(CreateProductionOrderCommand request, CancellationToken cancellationToken)
        {
            var userName = await _loginRepository.GetByName(request.UserName);
            if (userName == null) throw new EntityNotFoundException(nameof(Login), request.UserName);
            var order = new ProductionOrder(
                request.Priority,
                request.PlannedStartTime,
                request.PlannedEndTime,
                userName.LoginId,
                DateTime.Now
            );


            foreach (var item in request.Details)
            {
                var product = await _productRepository.GetById(item.ProductId);
                if (string.IsNullOrWhiteSpace(product.RecipeId))
                    throw new Exception($"Product {item.ProductId} has no recipe assigned");
                if (product == null)
                    throw new Exception($"Product {item.ProductId} not found");

                var recipeId = product.RecipeId; // lấy recipe từ product
                var recipe = await _recipeRepository.GetById(product.RecipeId);
                if (recipe == null)
                    throw new Exception($"Recipe {product.RecipeId} not found");

                var detail = order.AddDetail(
                    item.ProductId,
                    recipeId,
                    item.BatchQuantity,
                    item.SequenceNo
                );
                var snapshot = CreateRecipeSnapshot(product, recipe);
                detail.SetRecipeSnapshot(snapshot);
            }

            await _productionOrderRepository.AddAsync(order);
            return await _unitOfWork.SaveEntitiesAsync(cancellationToken);


        }

        private static RecipeSnapshotData CreateRecipeSnapshot(Product product, Recipe recipe)
        {
            return new RecipeSnapshotData
            {
                RecipeId = recipe.RecipeId,
                RecipeName = recipe.RecipeName,
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                SnapshotCreatedAt = DateTime.Now,
                Materials = recipe.RecipeMaterials.Select(rm => new RecipeSnapshotMaterialData
                {
                    MaterialId = rm.MaterialId,
                    MaterialName = rm.Material.MaterialName,
                    TargetKg = rm.TargetKg,
                    ToleranceMinKg = rm.ToleranceMinKg,
                    ToleranceMaxKg = rm.ToleranceMaxKg
                }).ToList()
            };
        }
    }
}
