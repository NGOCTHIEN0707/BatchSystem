using Domain.OrderBatchs;
using Domain.ProductionOrders.SnapShot;
using Domain.Products;
using Domain.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domain.ProductionOrders
{
    public class ProductionOrderDetail
    {
        public string ProductionOrderDetailId { get; private set; }
        public string ProductOrderId { get; private set; }
        public string ProductId { get; private set; }
        public string RecipeId { get; private set; }
        public int BatchQuantity { get; private set; }
        public int SequenceNo { get; private set; }
        public ProductionOrder ProductionOrder { get; private set; }
        public Product Product { get; private set; }
        public Recipe Recipe { get; private set; }
        public List<OrderBatch> OrderBatches { get; private set; } = new List<OrderBatch>();
        public string RecipeSnapshotJson { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ProductionOrderDetail()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ProductionOrderDetail(string productOrderId, string productId, string recipeId, int batchQuantity, int sequenceNo)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            ProductOrderId=productOrderId;
            ProductId=productId;
            RecipeId=recipeId;
            BatchQuantity=batchQuantity;
            SequenceNo=sequenceNo;
        }
        public void UpdateRecipeSnapshot(string recipeSnapshotJson)
        {
            RecipeSnapshotJson = recipeSnapshotJson;
        }
        public RecipeSnapshotData? GetRecipeSnapshot()
        {
            if (string.IsNullOrWhiteSpace(RecipeSnapshotJson))
                return null;
            return JsonSerializer.Deserialize<RecipeSnapshotData>(RecipeSnapshotJson)!;
        }

        public void SetRecipeSnapshot(RecipeSnapshotData snapshot)
        {
            RecipeSnapshotJson = JsonSerializer.Serialize(snapshot);
        }
    }
}
