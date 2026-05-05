using Domain.OrderBatchs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSystem.Domain.OrderBatchs.OrderBatchStatusHistories
{
    public class OrderBatchStatusHistory
    {
        public Guid OrderBatchId { get; private set; }
        public Guid OrderBatchStatusHistoryId { get; private set; }
        public DateTime ChangedAt { get; private set; }
        public OrderBatchStatus Status { get; private set; }
        public OrderBatchStatus? PreviousStatus { get; private set; }
        public OrderBatch? OrderBatch { get; private set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public OrderBatchStatusHistory()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public OrderBatchStatusHistory(Guid orderBatchId, DateTime changedAt, OrderBatchStatus status, OrderBatchStatus? previousStatus)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            OrderBatchId = orderBatchId;
            ChangedAt = changedAt;
            Status = status;
            PreviousStatus = previousStatus;
        }


    }
}
