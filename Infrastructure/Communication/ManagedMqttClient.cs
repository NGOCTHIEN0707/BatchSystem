using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client;


namespace BatchSystem.Infrastructure.Communication
{
    public class ManagedMqttClient : IManagedMqttClient, IDisposable
    {
        #region Dependencies
        private readonly ILogger<ManagedMqttClient> _logger;
        private readonly MqttOptions _options;
        #endregion

        #region State
        private bool _disposed;
        private readonly SemaphoreSlim _connectionLock = new(1, 1);
        private readonly HashSet<string> _subscribedTopics = new();
        private IMqttClient? _mqttClient;
        #endregion
        
        #region Public members
        public bool IsConnected => _mqttClient is not null && _mqttClient.IsConnected; // check kết nối client đã kết nối chưa
        public event Func<MqttMessage, Task>? MessageReceived;
        #endregion

        #region Constructor
        public ManagedMqttClient(ILogger<ManagedMqttClient> logger, IOptions<MqttOptions> options)
        {
            //_mqttClient = mqttClient ?? throw new ArgumentNullException(nameof(mqttClient));
            _options=options.Value;
            _logger=logger;
            _logger.LogDebug("ManagedMqttClient đã được khởi tạo thành công.");

        }
        #endregion

        #region Connection
        public async Task ConnectAsync(CancellationToken cancellationToken = default)
        {

            await _connectionLock.WaitAsync(cancellationToken);
            try
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(ManagedMqttClient));
                }
                if (IsConnected)
                {
                    _logger.LogInformation("Client đã được kết nối bởi một luồng khác. Bỏ qua.");
                    return;
                }
                // Client lúc này chưa kết nối cần tạo mới một clientOption
                _logger.LogInformation("Bắt đầu thử kết nối tới Broker...");

                if (_mqttClient is not null)
                {
                    try
                    {
                        if (_mqttClient.IsConnected)
                        {
                            await _mqttClient.DisconnectAsync();
                        }
                    }
                    catch
                    {
                    }

                    _mqttClient.Dispose();
                }

                _mqttClient = new MqttFactory().CreateMqttClient();
                var mqttClientOptions = new MqttClientOptionsBuilder()
                   .WithTcpServer(_options.Host, _options.Port)
                   .WithTimeout(TimeSpan.FromSeconds(_options.CommunicationTimeout))
                   .WithClientId(_options.ClientId)
                   .WithCredentials(_options.Username, _options.Password)
                   .WithKeepAlivePeriod(TimeSpan.FromSeconds(_options.KeepAliveInterval));

                _mqttClient.ApplicationMessageReceivedAsync += OnMessageReceivedAsync;
                _mqttClient.DisconnectedAsync += OnDisconnectedAsync;

                using var timeoutCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                timeoutCts.CancelAfter(TimeSpan.FromSeconds(_options.CommunicationTimeout));

                var result = await _mqttClient.ConnectAsync(mqttClientOptions.Build(), timeoutCts.Token);
                if (result.ResultCode != MqttClientConnectResultCode.Success)
                {
                    throw new InvalidOperationException($"Kết nối MQTT thất bại. ResultCode: {result.ResultCode}");

                }
                _logger.LogInformation("Kết nối MQTT thành công!");
                foreach (var topic in _subscribedTopics)
                {
                    await SubscribeInternalAsync(topic, cancellationToken);
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kết nối MQTT thất bại.");
                throw;
            }
            finally
            {
                _logger.LogDebug("Giải phóng Lock kết nối.");
                _connectionLock.Release();
            }

        }
        public async Task DisconnectAsync(CancellationToken cancellationToken = default)
        {
            await _connectionLock.WaitAsync(cancellationToken);
            try
            {
                if (_mqttClient is null)
                    return;

                if (_mqttClient.IsConnected)
                {
                    await _mqttClient.DisconnectAsync();
                    _logger.LogInformation("Đã ngắt kết nối MQTT.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ngắt kết nối MQTT thất bại.");
                throw;
            }
            finally
            {
                _connectionLock.Release();
            }
        }
        public async Task SubscribeAsync(string topic, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(topic))
                throw new ArgumentException("Topic không được để trống.", nameof(topic));

            _subscribedTopics.Add(topic);

            if (!IsConnected)
            {
                _logger.LogInformation("MQTT chưa connected. Đã lưu topic {Topic} để subscribe sau.", topic);
                return;
            }

            await SubscribeInternalAsync(topic, cancellationToken);
        }
        private async Task SubscribeInternalAsync(string topic, CancellationToken cancellationToken)
        {
            if (_mqttClient is null || !_mqttClient.IsConnected)
                throw new InvalidOperationException("MQTT Client chưa kết nối.");

            var topicFilter = new MqttTopicFilterBuilder()
                .WithTopic(topic)
                .Build();

            var subscribeOptions = new MqttClientSubscribeOptionsBuilder()
                .WithTopicFilter(topicFilter)
                .Build();

            var result = await _mqttClient.SubscribeAsync(subscribeOptions, cancellationToken);

            foreach (var item in result.Items)
            {
                if (item.ResultCode != MqttClientSubscribeResultCode.GrantedQoS0 &&
                    item.ResultCode != MqttClientSubscribeResultCode.GrantedQoS1 &&
                    item.ResultCode != MqttClientSubscribeResultCode.GrantedQoS2)
                {
                    throw new InvalidOperationException($"Subscribe topic '{topic}' thất bại: {item.ResultCode}");
                }
            }

            _logger.LogInformation("Subscribe topic thành công: {Topic}", topic);
        }

        #endregion


        #region Messaging
        public async Task PublishAsync(string topic, string payload, bool retainFlag = false, CancellationToken cancellationToken = default)
        {
            if (_mqttClient is null || !_mqttClient.IsConnected)
                throw new InvalidOperationException("MQTT Client chưa kết nối.");

            var applicationMessage = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(payload)
                .WithRetainFlag(retainFlag)
                .Build();

            var result = await _mqttClient.PublishAsync(applicationMessage, cancellationToken);

            if (result.ReasonCode != MqttClientPublishReasonCode.Success)
            {
                throw new InvalidOperationException($"Publish topic '{topic}' thất bại: {result.ReasonCode}");
            }

            _logger.LogInformation("Publish message thành công tới topic: {Topic}", topic);
        }
        private async Task OnMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs arg)
        {
            var topic = arg.ApplicationMessage.Topic ?? string.Empty;
            var payload = arg.ApplicationMessage.ConvertPayloadToString() ?? string.Empty;

            _logger.LogDebug("Nhận message từ topic: {Topic}", topic);

            if (MessageReceived is not null)
            {
                await MessageReceived.Invoke(new MqttMessage(topic, payload));
            }
        }
        #endregion


        #region LifeCycle
        private Task OnDisconnectedAsync(MqttClientDisconnectedEventArgs arg)
        {
            _logger.LogWarning("MQTT đã bị ngắt kết nối.");
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            _disposed = true;

            _mqttClient?.Dispose();
            _connectionLock.Dispose();
        }
        #endregion
    }
}
