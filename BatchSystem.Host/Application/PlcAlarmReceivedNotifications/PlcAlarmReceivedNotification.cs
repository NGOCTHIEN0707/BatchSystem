using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSystem.Host.Application.PlcAlarmReceivedNotifications
{
    public class PlcAlarmReceivedNotification : INotification
    {
        public MqttAlarmMessage Data { get; }
        public PlcAlarmReceivedNotification(MqttAlarmMessage data) => Data = data;
    }
}
