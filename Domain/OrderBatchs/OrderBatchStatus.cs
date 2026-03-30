using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.OrderBatchs
{
    public enum OrderBatchStatus
    {
        Pending = 0,     // Chưa chạy
        Waiting = 1,     // Đang chờ tới lượt
        Running = 2,     // Đang nằm trên line
        Paused = 3,      // Tạm dừng
        Completed = 4,   // Xong
        Cancelled = 5    // Hủy
    }
}
