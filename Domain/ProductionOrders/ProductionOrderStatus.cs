using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProductionOrders
{
    public enum ProductionOrderStatus
    {
        Pending = 0,     // Mới tạo
        Ready = 1,       // Sẵn sàng chạy
        Running = 2,     // Có ít nhất 1 batch đang chạy
        Paused = 3,      // Tạm dừng
        Held = 4,        // Dừng
        Stopped = 5,   // Hoàn tất
        Completed = 6,    // Hủy
        Aborted = 7,
        Cancelled = 8,
    }
}
