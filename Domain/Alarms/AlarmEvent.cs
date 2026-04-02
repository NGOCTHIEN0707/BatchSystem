using Domain.OrderBatchs;
using Domain.ProductionOrders;
using Domain.Stations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Alarms
{
    public class AlarmEvent
    {
        public Guid AlarmEventId { get; private set; }
        public string AlarmDefinitionId { get; private set; }
        public Guid? ProductionOrderId { get; private set; }
        public Guid? OrderBatchId { get; private set; }
        public string StationId { get; private set; }
        public DateTime OccurredAt { get; private set; }
        public DateTime? EndedAt { get; private set; }
        public DateTime? AckAt { get; private set; }
        public string? AckBy { get; private set; }
        public bool IsAcked { get; private set; } = false;
        public DateTime EventDate { get; private set; }
        public AlarmDefinition AlarmDefinition { get; private set; }
        public OrderBatch? OrderBatch { get; private set; }
        public ProductionOrder? ProductionOrder { get; private set; }
        public Station Station { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public AlarmEvent()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public AlarmEvent(string alarmDefinitionId, Guid? productionOrderId, Guid? orderBatchId, string stationId)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            AlarmDefinitionId = alarmDefinitionId;
            ProductionOrderId = productionOrderId;
            OrderBatchId = orderBatchId;
            StationId = stationId;
            OccurredAt = DateTime.Now;
            EventDate = DateTime.Today;
        }
    }
}
