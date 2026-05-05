using BatchSystem.Host.Application;
using BatchSystem.Host.Application.PlcAckReceivedNotifications;
using BatchSystem.Host.Application.PlcAlarmReceivedNotifications;
using BatchSystem.Host.Application.PlcProcessDataNotifications;
using BatchSystem.Host.Application.PlcStatusReceivedNotifications;
using BatchSystem.Infrastructure.Communication;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;


namespace BatchSystem.Host
{
    public class BatchStatusWorker : BackgroundService
    {
        private readonly IManagedMqttClient _mqttClient;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<BatchStatusWorker> _logger;
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        public BatchStatusWorker(IManagedMqttClient mqttClient, IServiceScopeFactory scopeFactory, ILogger<BatchStatusWorker> logger)
        {
            _mqttClient=mqttClient;
            _scopeFactory=scopeFactory;
            _logger=logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("BatchStatusWorker đang khởi động...");

            _mqttClient.MessageReceived -= OnMessageReceivedAsync;
            _mqttClient.MessageReceived += OnMessageReceivedAsync;

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    if (!_mqttClient.IsConnected)
                    {
                        _logger.LogWarning("MQTT chưa kết nối hoặc đã bị ngắt. Đang reconnect...");

                        await _mqttClient.ConnectAsync(stoppingToken);

                        await _mqttClient.SubscribeAsync("batchsystem/line1/alarm", stoppingToken);
                        await _mqttClient.SubscribeAsync("batchsystem/line1/status/runtime", stoppingToken);
                        await _mqttClient.SubscribeAsync("batchsystem/line1/ack", stoppingToken);
                        await _mqttClient.SubscribeAsync("batchsystem/line1/process_data", stoppingToken);

                        _logger.LogInformation("Đã reconnect MQTT và subscribe lại các topic.");
                    }
                }
                catch (OperationCanceledException)
                {
                    _logger.LogInformation("BatchStatusWorker đang dừng...");
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Reconnect MQTT thất bại. Sẽ thử lại sau...");
                }

                await Task.Delay(3000, stoppingToken);
            }
        }

        private async Task OnMessageReceivedAsync(MqttMessage message)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                var topic = message.Topic;
                var payload = message.Payload;

                if (string.IsNullOrWhiteSpace(topic) || string.IsNullOrWhiteSpace(payload))
                    return;

                switch (topic)
                {
                    case "batchsystem/line1/alarm":
                        var alarmMsg = JsonSerializer.Deserialize<MqttAlarmMessage>(payload, _jsonOptions);

                        if (alarmMsg != null)
                        {
                            await mediator.Publish(new PlcAlarmReceivedNotification(alarmMsg));
                        }
                        else
                        {
                            _logger.LogWarning("Không thể giải mã Alarm từ payload: {Payload}", payload);
                        }
                        break;

                    case "batchsystem/line1/status/runtime":
                        var statusMsg = JsonSerializer.Deserialize<MqttStatusMessage>(payload, _jsonOptions);

                        if (statusMsg == null)
                        {
                            _logger.LogWarning("Không parse được status payload: {Payload}", payload);
                            return;
                        }
                        await mediator.Publish(new PlcStatusReceivedNotification(statusMsg));
                        break;

                    case "batchsystem/line1/ack":
                        var data_ack = JsonSerializer.Deserialize<MqttAckMessage>(payload, _jsonOptions);

                        if (data_ack == null)
                        {
                            _logger.LogWarning("Không parse được process data payload: {Payload}", payload);
                            return;
                        }
                        await mediator.Publish(new PlcAckReceivedNotification(data_ack));
                        break;
                    case "batchsystem/line1/process_data":
                        var data_processValue = JsonSerializer.Deserialize<MqttProcessDataMessage>(payload, _jsonOptions);

                        if (data_processValue == null)
                        {
                            _logger.LogWarning("Không parse được process data payload: {Payload}", payload);
                            return;
                        }

                        await mediator.Publish(new PlcProcessDataNotification(
                            data_processValue.Raw1SP,
                            data_processValue.Raw1PV,
                            data_processValue.Raw2SP,
                            data_processValue.Raw2PV,
                            data_processValue.Raw3SP,
                            data_processValue.Raw3PV,
                            data_processValue.WaterSP,
                            data_processValue.WaterPV,
                            data_processValue.AdditiveSP,
                            data_processValue.AdditivePV,
                            data_processValue.Timestamp));
                        break;
                    default:
                        _logger.LogWarning("Topic không được hỗ trợ: {Topic}", topic);
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi xử lý message từ topic {Topic}", message.Topic);
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Đang dừng BatchStatusWorker...");

            _mqttClient.MessageReceived -= OnMessageReceivedAsync;

            await _mqttClient.DisconnectAsync(cancellationToken);

            await base.StopAsync(cancellationToken);
        }
    }
}
