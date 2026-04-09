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
    public string StationName { get; private set; }
    public string StationCode { get; private set; }
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
    public Station(string stationCode, string stationName, string lineId, int sequenceNo)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        StationCode = stationCode;
        StationName = stationName;
        LineId = lineId;
        SequenceNo = sequenceNo;
    }
    public void UpdateStationName(string stationName) => StationName = stationName;
    public void UpdateStationCode(string stationCode) => StationCode = stationCode;
    public void UpdateLineIdForStation(string lineId) => LineId = lineId;
    public void UpdateSequenceNo(int sequenceNo)
    {
        if (sequenceNo < 0) throw new Exception("Invalid SequenceNo for Station");
        SequenceNo = sequenceNo;
    }
}
