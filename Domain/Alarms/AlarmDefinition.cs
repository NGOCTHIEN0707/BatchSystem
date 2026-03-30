using Domain.Stations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Alarms
{
    public class AlarmDefinition
    {
        public string AlarmDefinitionId { get; private set; }
        public string StationId { get; private set; }
        public string AlarmText { get; private set; }
        public AlarmClass AlarmClass { get; private set; }
        public bool IsActive { get; private set; } = true;
        public Station Station { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public AlarmDefinition()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public AlarmDefinition(string stationId, string alarmText, AlarmClass alarmClass, bool isActive = true)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            StationId = stationId;
            AlarmText = alarmText;
            AlarmClass = alarmClass;
            IsActive = isActive;
        }
    }
}
