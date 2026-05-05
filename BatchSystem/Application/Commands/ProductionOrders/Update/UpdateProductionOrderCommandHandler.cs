using BatchSystem.Application.Exceptions;
using BatchSystem.Domain.ProductionOrders;
using BatchSystem.Domain.Products;
using BatchSystem.Domain.Recipes;
using BatchSystem.Domain.SeedWork;
using Domain.ProductionOrders;
using Domain.ProductionOrders.SnapShot;
using Domain.Products;
using Domain.Recipes;
using MediatR;
using static Domain.ProductionOrders.ProductionOrder;

namespace BatchSystem.Application.Commands.ProductionOrders.Update
{

    public class UpdateProductionOrderCommandHandler : IRequestHandler<UpdateProductionOrderCommand, bool>
    {
        private readonly IProductionOrderRepository _productionOrderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly IRecipeRepository _recipeRepository;

        public UpdateProductionOrderCommandHandler(IProductionOrderRepository productionOrderRepository, IUnitOfWork unitOfWork, IProductRepository productRepository, IRecipeRepository recipeRepository)
        {
            _productionOrderRepository=productionOrderRepository;
            _unitOfWork=unitOfWork;
            _productRepository=productRepository;
            _recipeRepository=recipeRepository;
        }

        public async Task<bool> Handle(UpdateProductionOrderCommand request, CancellationToken cancellationToken)
        {
            var productionOrderToUpdate = await _productionOrderRepository.GetById(request.ProductionOrderId);
            if (productionOrderToUpdate == null)
            {
                throw new EntityNotFoundException(nameof(ProductionOrder), request.ProductionOrderId.ToString());
            }
            if (productionOrderToUpdate.Status != ProductionOrderStatus.Pending)
            {
                throw new BusinessRuleException("Only pending production orders can be updated.");
            }
            if (request.Priority != null)
            {
                productionOrderToUpdate.UpdateProductionOrderPriority(request.Priority.Value);
            }
            productionOrderToUpdate.UpdateSchedule(request.PlannedStartTime, request.PlannedEndTime);
            if (request.Details!=null)
            {

                var detailInputs = new List<ProductionOrderDetailInput>();
                //var snapshotMap = new Dictionary<int, RecipeSnapshotData>();

                foreach (var item in request.Details)
                {
                    var product = await _productRepository.GetById(item.ProductId);

                    if (product == null)
                        throw new EntityNotFoundException(nameof(Product), item.ProductId);

                    if (string.IsNullOrWhiteSpace(product.RecipeId))
                        throw new BusinessRuleException($"Product {item.ProductId} has no recipe assigned.");

                    var recipe = await _recipeRepository.GetById(product.RecipeId);
                    if (recipe == null)
                        throw new EntityNotFoundException(nameof(Recipe), product.RecipeId);
                    var snapshot = CreateRecipeSnapshot(product, recipe);

                    detailInputs.Add(new ProductionOrderDetailInput
                    {
                        ProductionOrderDetailId = item.ProductionOrderDetailId,
                        ProductId = item.ProductId,
                        RecipeId = product.RecipeId,
                        BatchQuantity = item.NumberOfPieces,
                        SequenceNo = item.SequenceNo,
                        RecipeSnapshot = snapshot
                    });


                }                              
                productionOrderToUpdate.ReplaceDetails(detailInputs);
            }
            _productionOrderRepository.UpdateAsync(productionOrderToUpdate);
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
                GrindingTimeSeconds = recipe.GrindingTimeSeconds,
                MixingTimeSeconds = recipe.MixingTimeSeconds,
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
