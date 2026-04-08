using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.OrderBatchs
{
    public enum OrderBatchStatus
    {
        Pending,     // Chưa chạy
        Ready,     // Đang chờ tới lượt
        Running,     // Đang nằm trên line
        Paused,      // Tạm dừng
        Completed,   // Xong
        Cancelled,    // Hủy
        Aborted
    }
}
