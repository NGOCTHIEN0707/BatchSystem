using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSystem.Infrastructure.Communication
{
    public class MqttMessage
    {
        public string? Topic { get; set; }
        public string? Payload { get; set; }
        public DateTime ReceivedAt { get; }
        public MqttMessage(string topic, string payload)
        {
            Topic = topic;
            Payload = payload;
            ReceivedAt = DateTime.Now;
        }
    }
}
