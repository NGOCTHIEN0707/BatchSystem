using BatchSystem.Infrastructure.Communication;

namespace BatchSystem.Workers.Mqtt
{
    public class MqttRawMessageReceivedNotification : INotification
    {
        public string Topic { get; }
        public string Payload { get; }
        public DateTime ReceivedAt { get; }

        public MqttRawMessageReceivedNotification(string topic, string payload, DateTime receivedAt)
        {
            Topic = topic;
            Payload = payload;
            ReceivedAt = receivedAt;
        }

        public MqttRawMessageReceivedNotification(MqttMessage message)
        {
            Topic = message.Topic;
            Payload = message.Payload;
            ReceivedAt = message.ReceivedAt;
        }
    }
}
