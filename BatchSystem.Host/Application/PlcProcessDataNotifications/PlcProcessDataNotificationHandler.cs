using BatchSystem.Host.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BatchSystem.Host.Application.PlcProcessDataNotifications
{
    public class PlcProcessDataNotificationHandler : INotificationHandler<PlcProcessDataNotification>
    {
        //private readonly IHubContext<ProcessDataHub> _hubContext;
        private readonly IHttpClientFactory _httpClientFactory;
        public PlcProcessDataNotificationHandler(IHttpClientFactory httpClientFactory)//IHubContext<ProcessDataHub> hubContext)
        {
            //_hubContext=hubContext;
            _httpClientFactory = httpClientFactory;

        }

        public async Task Handle(PlcProcessDataNotification notification, CancellationToken cancellationToken)
        {
            try
            {
                Console.WriteLine("Publish to Hub");
                var client = _httpClientFactory.CreateClient("RealtimeApi");
                var data = new
                {
                    notification.Raw1SP,
                    notification.Raw1PV,
                    notification.Raw2SP,
                    notification.Raw2PV,
                    notification.Raw3SP,
                    notification.Raw3PV,
                    notification.WaterSP,
                    notification.WaterPV,
                    notification.AdditiveSP,
                    notification.AdditivePV,
                    notification.Timestamp
                };
                await client.PostAsJsonAsync(
                    "/internal/realtime/process-data",
                    data,
                    cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            //await _hubContext.Clients.All.SendAsync("processData", new
            //{
            //    notification.Raw1SP,
            //    notification.Raw1PV,
            //    notification.Raw2SP,
            //    notification.Raw2PV,
            //    notification.Raw3SP,
            //    notification.Raw3PV,
            //    notification.WaterSP,
            //    notification.WaterPV,
            //    notification.AdditiveSP,
            //    notification.AdditivePV,
            //    notification.Timestamp
            //}, cancellationToken);

        }
    }
}
