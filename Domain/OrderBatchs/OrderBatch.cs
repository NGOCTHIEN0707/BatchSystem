using BatchSystem.Domain.OrderBatchs;
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
        public Guid OrderBatchId { get; private set; }
        public Guid ProductionOrderId { get; private set; }
        public Guid ProductionOrderDetailId { get; private set; }
        public string? LineId { get; private set; }
        public int BatchNo { get; private set; }
        public OrderBatchStatus Status { get; private set; } = OrderBatchStatus.Pending;
        public string? CurrentStationId { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public ProductionOrder ProductionOrder { get; private set; }
        public ProductionOrderDetail ProductionOrderDetail { get; private set; }
        public Line Line { get; private set; }
        public Station? CurrentStation { get; private set; }
        public BatchStep CurrentStep { get; private set; } = BatchStep.None;
        public List<BatchWeighingResult> BatchWeighingResults { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public OrderBatch()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public OrderBatch(Guid productionOrderId, Guid productionOrderDetailId, int batchNo)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            ProductionOrderId = productionOrderId;
            ProductionOrderDetailId = productionOrderDetailId;
            BatchNo = batchNo;
        }
        public void AddWeighingResult(BatchWeighingResult result)
        {
            if (result == null)
                throw new Exception("BatchWeighingResult cannot be null.");

            BatchWeighingResults.Add(result);
        }
        public void UpdateCurrentStep(int stepNo)
        {
            if (!Enum.IsDefined(typeof(BatchStep), stepNo))
                throw new Exception("Invalid step value.");

            CurrentStep = (BatchStep)stepNo;
        }
        public void AssignLine(string lineId)
        {
            if (string.IsNullOrWhiteSpace(lineId))
                throw new Exception("LineId cannot be empty.");

            LineId = lineId;
        }
        public void Start()
        {
            if (Status != OrderBatchStatus.Ready)
                throw new Exception("Batch must be ready to start.");

            Status = OrderBatchStatus.Running;
        }

        public void Complete()
        {
            if (Status != OrderBatchStatus.Running)
                throw new Exception("Batch must be running to complete.");

            Status = OrderBatchStatus.Completed;
        }

        public void Cancell()
        {
            Status = OrderBatchStatus.Cancelled;
        }
    }
}
