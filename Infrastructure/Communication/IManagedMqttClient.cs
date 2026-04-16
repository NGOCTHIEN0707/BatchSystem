using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSystem.Infrastructure.Communication
{
    public interface IManagedMqttClient
    {
        event Func<MqttMessage, Task>? MessageReceived;
        bool IsConnected { get; }
        Task ConnectAsync(CancellationToken cancellationToken = default);
        Task DisconnectAsync(CancellationToken cancellationToken = default);
        Task SubscribeAsync(string topic, CancellationToken cancellationToken = default);
        Task PublishAsync(string topic, string payload, bool retainFlag = false, CancellationToken cancellationToken = default);
    }
}
