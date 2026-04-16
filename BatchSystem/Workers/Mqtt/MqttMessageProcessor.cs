using BatchSystem.Infrastructure.Communication;

namespace BatchSystem.Workers.Mqtt
{
    public class MqttMessageProcessor : IMqttMessageProcessor
    {
        private readonly IMediator _mediator;

        public MqttMessageProcessor(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task HandleAsync(MqttMessage message, CancellationToken cancellationToken)
        {
            var notification = new MqttRawMessageReceivedNotification(message);
            await _mediator.Publish(notification, cancellationToken);
        }
    }
}
