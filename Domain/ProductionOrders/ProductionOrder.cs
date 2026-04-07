using Domain.Alarms;
using Domain.OrderBatchs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProductionOrders
{
    public class ProductionOrder
    {
        public Guid ProductionOrderId { get; private set; }
        public int Priority { get; private set; }
        public ProductionOrderStatus Status { get; private set; } = ProductionOrderStatus.Pending;
        public DateTime? PlannedStartTime { get; private set; }
        public DateTime? PlannedEndTime { get; private set; }
        public DateTime? ActualStartTime { get; private set; }
        public DateTime? ActualEndTime { get; private set; }
        public string CreatedBy { get; private set; } 
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<ProductionOrderDetail> ProductionOrderDetails { get; private set; } = new List<ProductionOrderDetail>();
        //public List<OrderBatch> OrderBatches { get; private set; } = new List<OrderBatch>();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ProductionOrder()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ProductionOrder(int priority, DateTime? plannedStartTime, DateTime? plannedEndTime, string createdBy, DateTime createdAt)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Priority=priority;
            PlannedStartTime=plannedStartTime;
            PlannedEndTime=plannedEndTime;
            CreatedBy=createdBy;
            CreatedAt=createdAt;
        }
        public ProductionOrderDetail AddDetail(string productId, string recipeId, int batchQuantity, int sequenceNo)
        {
            if (string.IsNullOrWhiteSpace(productId))
                throw new ArgumentException("ProductId is required.");

            if (string.IsNullOrWhiteSpace(recipeId))
                throw new ArgumentException("RecipeId is required.");

            if (batchQuantity <= 0)
                throw new ArgumentException("BatchQuantity must be greater than 0.");

            if (sequenceNo <= 0)
                throw new ArgumentException("SequenceNo must be greater than 0.");

            if (ProductionOrderDetails.Any(x => x.SequenceNo == sequenceNo))
                throw new InvalidOperationException($"SequenceNo {sequenceNo} already exists.");

            var detail = new ProductionOrderDetail(
                ProductionOrderId,
                productId,
                recipeId,
                batchQuantity,
                sequenceNo
            );

            ProductionOrderDetails.Add(detail);
            return detail;
        }
    }
}
