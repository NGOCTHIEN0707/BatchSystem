using Domain.Alarms;
using Domain.Lines;
using Domain.OrderBatchs.BatchWeightResults;
using Domain.ProductionOrders;
using Domain.Stations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.OrderBatchs
{
    public class OrderBatch
    {
        public string OrderBatchId { get; private set; }
        public string ProductionOrderId { get; private set; }
        public string ProductionOrderDetailId { get; private set; }
        public string LineId { get; private set; }
        public int BatchNo { get; private set; }
        public OrderBatchStatus Status { get; private set; } = OrderBatchStatus.Pending;
        public string? CurrentStationId { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public ProductionOrder ProductionOrder { get; private set; }
        public ProductionOrderDetail ProductionOrderDetail { get; private set; }
        public Line Line { get; private set; }
        public Station? CurrentStation { get; private set; }
        public List<OrderBatchStep> OrderBatchSteps { get; private set; } = new List<OrderBatchStep>();
        public List<BatchWeighingResult> BatchWeighingResults { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public OrderBatch()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public OrderBatch(string productionOrderId, string productionOrderDetailId, string lineId, int batchNo)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            ProductionOrderId = productionOrderId;
            ProductionOrderDetailId = productionOrderDetailId;
            LineId = lineId;
            BatchNo = batchNo;
        }
    }
}
