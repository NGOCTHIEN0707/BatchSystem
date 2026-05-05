using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSystem.Host.Application.PlcStatusReceivedNotifications
{
    public class PlcStatusReceivedNotification : INotification
    {
        public MqttStatusMessage MqttStatusMessage { get; set; }

        public PlcStatusReceivedNotification(MqttStatusMessage mqttStatusMessage)
        {
            MqttStatusMessage=mqttStatusMessage;
        }
    }
}
