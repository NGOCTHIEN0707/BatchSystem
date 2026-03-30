using Domain.Alarms;
using Domain.Lines;
using Domain.OrderBatchs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Stations;
public class Station
{
    public string StationId { get; private set; }
    public string StationTypeId { get; private set; }
    public string StationName { get; private set; }
    public StationType StationType { get; private set; }
    public Line Line { get; private set; }
    public string LineId { get; private set; }
    public int SequenceNo { get; private set; }
    public List<AlarmDefinition> AlarmDefinitions { get; private set; } = new List<AlarmDefinition>();
    public StationCurrentStatus? CurrentStatus { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Station()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Station(string stationTypeId, string stationName, string lineId, int sequenceNo)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        StationTypeId = stationTypeId;
        StationName = stationName;
        LineId = lineId;
        SequenceNo = sequenceNo;
    }
}
