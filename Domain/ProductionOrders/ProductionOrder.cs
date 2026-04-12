using Domain.Alarms;
using Domain.Logins;
using Domain.OrderBatchs;
using Domain.ProductionOrders.SnapShot;
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
        public string? CustomerLoginId { get; private set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Login? CustomerLogin { get; private set; }

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
            CustomerLoginId =createdBy;
            CreatedAt=createdAt;
        }

        public void ConfirmReady()
        {
            if (Status != ProductionOrderStatus.Pending)
                throw new Exception("Only pending production order can be changed to ready.");

            if (!ProductionOrderDetails.Any())
                throw new Exception("Production order must have at least one detail.");

            foreach (var detail in ProductionOrderDetails.OrderBy(x => x.SequenceNo))
            {
                detail.CreateOrderBatches();
            }

            Status = ProductionOrderStatus.Ready;
        }
        public void Start()
        {
            if (Status != ProductionOrderStatus.Ready &&
                Status != ProductionOrderStatus.Paused &&
                Status != ProductionOrderStatus.Stopped)
            throw new Exception("Production order cannot be started.");

            Status = ProductionOrderStatus.Running;
        }
        public void Complete()
        {
            if (Status != ProductionOrderStatus.Running)
                throw new Exception("Only running production order can be completed.");

            if (!ProductionOrderDetails.SelectMany(x => x.OrderBatches)
                .All(x => x.Status == OrderBatchStatus.Completed))
            {
                throw new Exception("All batches must be completed.");
            }

            Status = ProductionOrderStatus.Completed;

        }
        public void Pause()
        {
            if (Status != ProductionOrderStatus.Running)
                throw new Exception("Only running order can be paused.");

            Status = ProductionOrderStatus.Paused;
        }
        public void Stop()
        {
            if (Status != ProductionOrderStatus.Running &&
                Status != ProductionOrderStatus.Paused)
                throw new Exception("Order cannot be stopped.");

            Status = ProductionOrderStatus.Stopped;
        }
        public void Cancel()
        {
            if (Status != ProductionOrderStatus.Pending)
                throw new Exception("Only pending production order can be cancelled.");

            Status = ProductionOrderStatus.Cancelled;
        }
        public void Abort()
        {
            if (Status != ProductionOrderStatus.Running &&
                Status != ProductionOrderStatus.Paused &&
                Status != ProductionOrderStatus.Held)
                throw new Exception("Only active order can be aborted.");

            Status = ProductionOrderStatus.Aborted;
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
        public void UpdateProductionOrderPriority(int priority) => Priority=priority;
        public void UpdateProductionOrderStatus(ProductionOrderStatus status) => Status=status;
        public void UpdateProductionOrderActualStartTime(DateTime actualStartTime) => ActualStartTime=actualStartTime;
        public void UpdateProductionOrderActualEndTime(DateTime actualEndTime) => ActualEndTime=actualEndTime;

        public void UpdateSchedule(DateTime? newStart, DateTime? newEnd)
        {
            // Giữ nguyên giá trị cũ nếu tham số truyền vào là null
            DateTime? effectiveStart = newStart ?? PlannedStartTime;
            DateTime? effectiveEnd = newEnd ?? PlannedEndTime;

            if (effectiveStart > effectiveEnd)
                throw new Exception("Lịch trình không hợp lệ.");

            PlannedStartTime = effectiveStart;
            PlannedEndTime = effectiveEnd;
        }

        public void ReplaceDetails(List<ProductionOrderDetailInput> newDetails)
        {
            if (newDetails == null || !newDetails.Any())
                throw new ArgumentException("Production order must have at least one detail.");

            if (newDetails == null || !newDetails.Any())
                throw new ArgumentException("Production order must have at least one detail.");

            var incomingIds = newDetails
                .Where(x => x.ProductionOrderDetailId != Guid.Empty)
                .Select(x => x.ProductionOrderDetailId)
                .ToHashSet();

            var detailsToRemove = ProductionOrderDetails
                .Where(x => x.ProductionOrderDetailId != Guid.Empty &&
                            !incomingIds.Contains(x.ProductionOrderDetailId))
                .ToList();

            foreach (var detail in detailsToRemove)
            {
                ProductionOrderDetails.Remove(detail);
            }
            foreach (var item in newDetails.OrderBy(x => x.SequenceNo))
            {
                if (item.ProductionOrderDetailId != Guid.Empty)
                {
                    var existingDetail = ProductionOrderDetails
                        .FirstOrDefault(x => x.ProductionOrderDetailId == item.ProductionOrderDetailId);

                    if (existingDetail == null)
                        throw new ArgumentException($"Detail Id '{item.ProductionOrderDetailId}' not found.");

                    // Cập nhật thông tin
                    existingDetail.UpdateProductId(item.ProductId);
                    existingDetail.UpdateRecipeId(item.RecipeId);
                    existingDetail.UpdateBatchQuantity(item.BatchQuantity);
                    existingDetail.UpdateSequenceNo(item.SequenceNo);

                    // Gán Snapshot (đã được chuẩn bị từ Handler)
                    existingDetail.SetRecipeSnapshot(item.RecipeSnapshot);
                }
                else
                {
                    // Thêm mới hoàn toàn
                    var newDetail = AddDetail(
                        item.ProductId,
                        item.RecipeId,
                        item.BatchQuantity,
                        item.SequenceNo
                    );

                    newDetail.SetRecipeSnapshot(item.RecipeSnapshot);
                }
            }
        }
        public class ProductionOrderDetailInput
        {
            public Guid ProductionOrderDetailId { get; set; }
            public string ProductId { get; set; } = string.Empty;
            public string RecipeId { get; set; } = string.Empty;
            public int BatchQuantity { get; set; }
            public int SequenceNo { get; set; }
            public RecipeSnapshotData RecipeSnapshot { get; set; }
        }

    }
}
