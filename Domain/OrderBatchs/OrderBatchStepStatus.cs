using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.OrderBatchs
{
    public enum OrderBatchStepStatus
    {
        Pending = 0,       // Chưa tới step này
        Waiting = 1,       // Đã tới nhưng chưa chạy
        Processing = 2,    // Đang xử lý tại station
        Completed = 3,     // Xong step
        Fault = 4,         // Lỗi tại step
        Blocked = 5        // Không chuyển đi được
    }
}
