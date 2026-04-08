using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSystem.Domain.OrderBatchs
{
    public enum BatchStep
    {
        None = 0,
        PrepareMaterial = 1,
        WeighingAndConveyor = 2,
        Grinding = 3,
        Mixing = 4,
        Discharge = 5
    }
}
