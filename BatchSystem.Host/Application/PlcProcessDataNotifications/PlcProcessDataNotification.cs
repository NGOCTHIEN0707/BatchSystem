using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSystem.Host.Application.PlcProcessDataNotifications
{
    public class PlcProcessDataNotification : INotification
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

        public PlcProcessDataNotification(float raw1SP, float raw1PV, float raw2SP, float raw2PV, float raw3SP, float raw3PV, float waterSP, float waterPV, float additiveSP, float additivePV, DateTime timestamp)
        {
            Raw1SP=raw1SP;
            Raw1PV=raw1PV;
            Raw2SP=raw2SP;
            Raw2PV=raw2PV;
            Raw3SP=raw3SP;
            Raw3PV=raw3PV;
            WaterSP=waterSP;
            WaterPV=waterPV;
            AdditiveSP=additiveSP;
            AdditivePV=additivePV;
            Timestamp=timestamp;
        }
    }
}
