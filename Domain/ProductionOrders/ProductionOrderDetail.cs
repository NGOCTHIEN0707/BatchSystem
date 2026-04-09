using Domain.Lines;
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
        public Guid ProductionOrderDetailId { get; private set; }
        public Guid ProductionOrderId { get; private set; }
        public string ProductId { get; private set; }
        public string RecipeId { get; private set; }
        public int BatchQuantity { get; private set; }
        public int SequenceNo { get; private set; }// Thứ tự chạy
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
        public ProductionOrderDetail(Guid productionOrderId, string productId, string recipeId, int batchQuantity, int sequenceNo)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            ProductionOrderId=productionOrderId;
            ProductId=productId;
            RecipeId=recipeId;
            BatchQuantity=batchQuantity;
            SequenceNo=sequenceNo;
        }

        public void CreateOrderBatches()
        {
            if (BatchQuantity <= 0)
                throw new Exception("BatchQuantity must be greater than zero.");


            var batches = new List<OrderBatch>();

            for (int i = 1; i <= BatchQuantity; i++)
            {
                batches.Add(new OrderBatch(
                    ProductionOrderId,
                    ProductionOrderDetailId,
                    i
                )); 
            }
            OrderBatches = batches;
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

        public void UpdateProductId(string productId)
        {
            if (string.IsNullOrWhiteSpace(productId))
                throw new Exception("ProductId cannot be empty.");

            ProductId = productId;
        }
        public void UpdateRecipeId(string recipeId)
        {
            if (string.IsNullOrWhiteSpace(recipeId))
                throw new Exception("RecipeId cannot be empty.");

            RecipeId = recipeId;
        }
        public void UpdateBatchQuantity(int batchQuantity)
        {
            if (batchQuantity <= 0)
                throw new Exception("Batch quantity must be greater than zero.");

            BatchQuantity = batchQuantity;
        }
        public void UpdateSequenceNo(int sequenceNo)
        {
            if (sequenceNo < 0)
                throw new Exception("Sequence number cannot be negative.");

            SequenceNo = sequenceNo;
        }

    }
}
