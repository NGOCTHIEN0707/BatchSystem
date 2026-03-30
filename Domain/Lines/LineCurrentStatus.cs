using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Lines
{
    public class LineCurrentStatus
    {
        public string LineCurrentStatusId { get; private set; }
        public string LineId { get; private set; }
        public LineStatus Status { get; private set; }
        public LineMode Mode { get; private set; }
        public DateTime UpdatedAt { get; private set; } = DateTime.Now;
        public string UpdatedBy { get; private set; }
        public Line Line { get; private set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public LineCurrentStatus()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public LineCurrentStatus(string lineId, LineStatus status, LineMode mode, string updatedBy)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            LineId = lineId;
            Status = status;
            Mode = mode;
            UpdatedBy = updatedBy;
        }
    }
}
