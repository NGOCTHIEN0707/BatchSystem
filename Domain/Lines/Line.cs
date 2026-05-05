using Domain.OrderBatchs;
using Domain.Stations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Lines;
public class Line
{
    public string LineId { get; private set; }
    public string LineName { get; private set; }
    public string LineCode { get; private set; }
    public List<Station> Stations { get; private set; } = new List<Station>();
    //public LineCurrentStatus? CurrentStatus { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Line()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Line(string lineName, string lineCode)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        LineName = lineName;
        LineCode = lineCode;
    }
    public void UpdateLineName( string lineName) => LineName = lineName;
    public void UpdateLineCode(string lineCode) => LineCode = lineCode;
}
