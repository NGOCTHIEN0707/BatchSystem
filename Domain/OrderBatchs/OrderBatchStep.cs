using Domain.Stations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.OrderBatchs
{
    public class OrderBatchStep
    {
        public string OrderBatchStepId { get; private set; }
        public string OrderBatchId { get; private set; }
        public string StationId { get; private set; }
        public int StepNo { get; private set; }
        public OrderBatchStepStatus Status { get; private set; } = OrderBatchStepStatus.Pending;
        public DateTime? EnteredAt { get; private set; }
        public DateTime? StartedAt { get; private set; }
        public DateTime? CompletedAt { get; private set; }
        public OrderBatch OrderBatch { get; private set; }
        public Station Station { get; private set; } 


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public OrderBatchStep()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public OrderBatchStep(string orderBatchId, string stationId, int stepNo)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            OrderBatchId = orderBatchId;
            StationId = stationId;
            StepNo = stepNo;
        }
    }
}
