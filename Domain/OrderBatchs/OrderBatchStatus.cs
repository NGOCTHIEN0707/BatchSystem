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
            Sent,         // đã publish xuống PLC
            Received,   // PLC đã nhận ACK
            Running,     // Đang nằm trên line
            WeighingCompleted, // Khi đã cân xong các nguyên liệu
            Paused,      // Tạm dừng
            Completed,   // Xong
            Cancelled,    // Hủy
            Aborted,
            Error   //runtime
        }
    }
