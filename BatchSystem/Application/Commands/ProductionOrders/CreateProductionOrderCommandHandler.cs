using BatchSystem.Domain.ProductionOrders;
using BatchSystem.Domain.Products;
using BatchSystem.Domain.Recipes;
using Domain.ProductionOrders;
using Domain.ProductionOrders.SnapShot;
using Domain.Products;
using Domain.Recipes;
using MediatR;

namespace BatchSystem.Application.Commands.ProductionOrders
{
    public class CreateProductionOrderCommandHandler : IRequestHandler<CreateProductionOrderCommand, bool>
    {
        private readonly IProductionOrderRepository _productionOrderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IRecipeRepository _recipeRepository;

        public CreateProductionOrderCommandHandler(IProductionOrderRepository productionOrderRepository, IProductRepository productRepository, IRecipeRepository recipeRepository)
        {
            _productionOrderRepository=productionOrderRepository;
            _productRepository=productRepository;
            _recipeRepository=recipeRepository;
        }

        public async Task<bool> Handle(CreateProductionOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new ProductionOrder(
                request.Priority,
                request.PlannedStartTime,
                request.PlannedEndTime,
                request.CreatedBy,
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
            return await _productionOrderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);


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
