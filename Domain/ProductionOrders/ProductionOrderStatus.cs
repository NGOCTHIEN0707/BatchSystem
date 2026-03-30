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
        Stop = 4,        // Dừng
        Completed = 5,   // Hoàn tất
        Cancelled = 6    // Hủy
    }
}
