using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Lines
{
    public enum LineStatus
    {
        Stop = 0,        // Dừng hoàn toàn
        Run = 1,         // Đang chạy
        Hold = 2,        // Tạm giữ (chờ điều kiện)
        Pause = 3,          //operation action
        Abort = 4,       // Dừng khẩn cấp có kiểm soát
        Emergency = 5    // Dừng khẩn cấp (E-Stop)

    }
}
