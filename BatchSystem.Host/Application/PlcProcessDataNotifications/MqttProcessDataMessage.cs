using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSystem.Host.Application.PlcProcessDataNotifications
{
    public class MqttProcessDataMessage
    {
        public float Raw1SP { get; set; }
        public float Raw1PV { get; set; }

        public float Raw2SP { get; set; }
        public float Raw2PV { get; set; }

        public float Raw3SP { get; set; }
        public float Raw3PV { get; set; }

        public float WaterSP { get; set; }
        public float WaterPV { get; set; }

        public float AdditiveSP { get; set; }
        public float AdditivePV { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
