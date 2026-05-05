using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSystem.Host.Application.PlcAckReceivedNotifications
{
    public class PlcAckReceivedNotification : INotification
    {
        public MqttAckMessage AckMessage { get; set; }

        public PlcAckReceivedNotification(MqttAckMessage ackMessage)
        {
            AckMessage=ackMessage;
        }
    }
}
