using BatchSystem.Infrastructure.Communication;

namespace BatchSystem.Workers.Mqtt
{
    public interface IMqttMessageProcessor
    {
        Task HandleAsync(MqttMessage message, CancellationToken cancellationToken);
    }
}
