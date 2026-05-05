using BatchSystem.Host.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace BatchSystem.Host.Application.PlcStatusReceivedNotifications
{
    public class PlcStatusReceivedNotificationHandler : INotificationHandler<PlcStatusReceivedNotification>
    {
        //private readonly IHubContext<StatusHub> _hubContext;
        private readonly IHttpClientFactory _httpClientFactory;
        //public PlcStatusReceivedNotificationHandler(IHubContext<StatusHub> hubContext)
        //{
        //    _hubContext=hubContext;
        //}

        public PlcStatusReceivedNotificationHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task Handle(PlcStatusReceivedNotification notification, CancellationToken cancellationToken)
        {
            var s = notification.MqttStatusMessage;

            var isSameBatch =
                s.ActiveBatchNo != 0 &&
                s.ActiveBatchNo == s.ProcessingBatchNo;

            var hasActiveBatch =
                !isSameBatch &&
                s.ActiveOrderBatchId.HasValue &&
                s.ActiveBatchNo != 0;

            var hasProcessingBatch =
                s.ProcessingOrderBatchId.HasValue &&
                s.ProcessingBatchNo != 0;

            var activeProgress = hasActiveBatch
                ? GetProgress(s.ActiveBatchStepNo)
                : 0;

            var processingProgress = hasProcessingBatch
                ? GetProgress(s.ProcessingBatchStepNo)
                : 0;


            Console.WriteLine("Publish status to API");

            var client = _httpClientFactory.CreateClient("RealtimeApi");

            var data = new
            {
                s.ProductionOrderDetailId,

                Active = new
                {
                    HasBatch = hasActiveBatch,
                    OrderBatchId = hasActiveBatch ? s.ActiveOrderBatchId : null,
                    BatchNo = hasActiveBatch ? s.ActiveBatchNo : 0,
                    StepNo = hasActiveBatch ? s.ActiveBatchStepNo : (ushort)0,
                    PhaseState = hasActiveBatch ? s.ActiveBatchPhaseState : (ushort)0,
                    ProgressPercent = activeProgress
                },

                Processing = new
                {
                    HasBatch = hasProcessingBatch,
                    OrderBatchId = hasProcessingBatch ? s.ProcessingOrderBatchId : null,
                    BatchNo = hasProcessingBatch ? s.ProcessingBatchNo : 0,
                    StepNo = hasProcessingBatch ? s.ProcessingBatchStepNo : (ushort)0,
                    PhaseState = hasProcessingBatch ? s.ProcessingBatchPhaseState : (ushort)0,
                    ProgressPercent = processingProgress
                },

                s.OperationMode,
                s.ControlState,
                s.BatchState,

                s.AlarmSummaryWord,
                s.ActiveAlarmCount,
                s.Heartbeat,
                s.Timestamp
            };

            var response = await client.PostAsJsonAsync(
                "/internal/realtime/status",
                data,
                cancellationToken);

            response.EnsureSuccessStatusCode();


            //await _hubContext.Clients.All.SendAsync(
            //    "StatusUpdated",
            //    new
            //    {
            //        s.OrderBatchId,
            //        s.ProductionOrderDetailId,
            //        s.BatchNo,

            //        s.OperationMode,
            //        s.ControlState,
            //        s.BatchState,

            //        s.StepNo,
            //        s.PhaseState,
            //        ProgressPercent = progressPercent,

            //        s.AlarmSummaryWord,
            //        s.ActiveAlarmCount,
            //        s.Heartbeat,

            //        s.Timestamp
            //    },
            //    cancellationToken
            //);
        }
        private static int GetProgress(int step)
        {
            if (step <= 0) return 0;
            if (step >= 5) return 100;
            return (int)((double)step / 5 * 100);
        }
    }
}
