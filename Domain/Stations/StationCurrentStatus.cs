using Domain.OrderBatchs;
using Domain.ProductionOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Stations
{
    public class StationCurrentStatus
    {
        public string StationCurrentStatusId { get; private set; }
        public string StationId { get; private set; }
        public StationState State { get; private set; }
        public string? CurrentBatchId { get; private set; }
        public string? CurrentOrderId { get; private set; }
        public DateTime UpdatedAt { get; private set; } = DateTime.Now;
        public Station Station { get; private set; }
        public OrderBatch? CurrentBatch { get; private set; }
        public ProductionOrder? CurrentOrder { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public StationCurrentStatus()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public StationCurrentStatus(string stationId, StationState state, string? currentBatchId, string? currentOrderId)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            StationId = stationId;
            State = state;
            CurrentBatchId = currentBatchId;
            CurrentOrderId = currentOrderId;
        }
    }
}
