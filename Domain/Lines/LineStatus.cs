using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Lines
{
    public enum LineStatus : ushort
    {
        Idle = 0,
        Running = 1,
        Aborting = 2,
        Stopping = 3,
        Pausing = 4,
        Holding = 5,
        Complete = 6

    }
}
