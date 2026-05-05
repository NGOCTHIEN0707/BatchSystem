using BatchSystem.Domain.Alarms;
using BatchSystem.Domain.OrderBatchs;
using BatchSystem.Domain.SeedWork;
using BatchSystem.Host.Hubs;
using Domain.Alarms;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace BatchSystem.Host.Application.PlcAlarmReceivedNotifications
{
    public class PlcAlarmReceivedNotificationHandler : INotificationHandler<PlcAlarmReceivedNotification>
    {
        private readonly IAlarmDefinitionRepository _alarmDefinitionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAlarmEventRepository _alarmEventRepository;
        private readonly ILogger<PlcAlarmReceivedNotificationHandler> _logger;
        //private readonly IHubContext<AlarmHub> _hubContext;
        private readonly IHttpClientFactory _httpClientFactory;


        //public PlcAlarmReceivedNotificationHandler(IAlarmDefinitionRepository alarmDefinitionRepository, IUnitOfWork unitOfWork, IAlarmEventRepository alarmEventRepository, ILogger<PlcAlarmReceivedNotificationHandler> logger, IHubContext<AlarmHub> hubContext)
        //{
        //    _alarmDefinitionRepository=alarmDefinitionRepository;
        //    _unitOfWork=unitOfWork;
        //    _alarmEventRepository=alarmEventRepository;
        //    _logger=logger;
        //    _hubContext=hubContext;
        //}
        public PlcAlarmReceivedNotificationHandler(
           IAlarmDefinitionRepository alarmDefinitionRepository,
           IUnitOfWork unitOfWork,
           IAlarmEventRepository alarmEventRepository,
           ILogger<PlcAlarmReceivedNotificationHandler> logger,
           IHttpClientFactory httpClientFactory)
        {
            _alarmDefinitionRepository = alarmDefinitionRepository;
            _unitOfWork = unitOfWork;
            _alarmEventRepository = alarmEventRepository;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task Handle(PlcAlarmReceivedNotification notification, CancellationToken cancellationToken)
        {
            var data = notification.Data;
            
            try
            {
                Guid? orderBatchId = data.OrderBatchId;
                Guid? productionOrderId = data.ProductionOrderId;
                //lưu DB
                if (data.EventType == "Raised")
                {
                    // 1. Check null an toàn cho Definition
                    var alarmDefinition = await _alarmDefinitionRepository.GetById(data.AlarmId);
                    if (alarmDefinition == null)
                    {
                        _logger.LogWarning("AlarmId {Id} không tồn tại trong Definition", data.AlarmId);
                        return;
                    }

                    // 3. Lưu DB bản ghi mới
                    var alarmEvent = new AlarmEvent(
                        data.AlarmId,
                        productionOrderId,
                        orderBatchId,
                        alarmDefinition.StationId,
                        data.Timestamp // Dùng timestamp từ Gateway cho đồng bộ
                    );

                    await _alarmEventRepository.AddAsync(alarmEvent);
                }
                else if (data.EventType == "Cleared")
                {
                    // 4. Xử lý đóng lỗi (Cực kỳ quan trọng)
                    var activeEvent = await _alarmEventRepository.GetActiveEventAsync(data.AlarmId, orderBatchId);
                    if (activeEvent != null)
                    {
                        activeEvent.MarkAsEnded(data.Timestamp);
                         _alarmEventRepository.UpdateAsync(activeEvent);
                    }
                }
                await _unitOfWork.SaveEntitiesAsync(cancellationToken);

                var client = _httpClientFactory.CreateClient("RealtimeApi");

                var realtimeData = new
                {
                    data.AlarmId,
                    data.AlarmName,
                    data.EventType,
                    data.Timestamp,
                    data.OrderBatchId,
                    data.ProductionOrderId
                };

                var response = await client.PostAsJsonAsync(
                    "/internal/realtime/alarm",
                    realtimeData,
                    cancellationToken);

                response.EnsureSuccessStatusCode();

                Console.WriteLine("Publish alarm to API success");
                //await _hubContext.Clients.All.SendAsync("ReceiveAlarmUpdate", new
                //{
                //    data.AlarmId,
                //    data.AlarmName,
                //    data.EventType,
                //    data.Timestamp,
                //    data.OrderBatchId,
                //    data.ProductionOrderId,
                //}, cancellationToken);
                //Console.WriteLine("Publish Alarm To Hub Successe");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi xử lý Alarm {Id}", data.AlarmId);
            }
        }
    }
}
