using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Stations
{
    public enum StationState
    {
        Idle = 0,        // Rảnh, sẵn sàng nhận batch
        Processing = 1,  // Đang xử lý batch
        Blocked = 2,     // Bị kẹt, chưa chuyển batch đi được
        Fault = 3        // Lỗi
    }
}
