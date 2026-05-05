using Domain.ProductionOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSystem.Domain.ProductionOrders.ProductionOrderStatusHistories
{
    public class ProductionOrderStatusHistory
    {
        public Guid ProductionOrderStatusHistoryId { get; private set; }
        public Guid ProductionOrderId { get; private set; }
        public DateTime ChangedAt { get; private set; }
        public ProductionOrderStatus Status { get; private set; }
        public ProductionOrderStatus? PreviousStatus { get; private set; }

        public ProductionOrder? ProductionOrder { get; private set; }

        public ProductionOrderStatusHistory() { }

        public ProductionOrderStatusHistory(
            Guid productionOrderId,
            DateTime changedAt,
            ProductionOrderStatus status,
            ProductionOrderStatus? previousStatus)
        {
            ProductionOrderId = productionOrderId;
            ChangedAt = changedAt;
            Status = status;
            PreviousStatus = previousStatus;
        }
    }
}
