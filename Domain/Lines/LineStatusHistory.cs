using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Lines
{
    public class LineStatusHistory
    {
        public Guid LineStatusHistoryId { get; private set; }
        public string LineId { get; private set; }
        public LineStatus Status { get; private set; }
        public LineMode Mode { get; private set; }
        public DateTime ChangedAt { get; private set; } = DateTime.Now;
        public string ChangedBy { get; private set; }
        public Line Line { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public LineStatusHistory()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public LineStatusHistory(string lineId, LineStatus status, LineMode mode, string changedBy)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            LineId = lineId;
            Status = status;
            Mode = mode;
            ChangedBy = changedBy;
        }
    }
}
