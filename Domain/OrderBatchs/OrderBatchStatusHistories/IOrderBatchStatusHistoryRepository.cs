using Domain.OrderBatchs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSystem.Domain.OrderBatchs.OrderBatchStatusHistories
{
    public interface IOrderBatchStatusHistoryRepository
    {
        Task AddAsync(OrderBatchStatusHistory orderBatchStatusHistory);
        void Delete(OrderBatchStatusHistory orderBatchStatusHistory);
        Task<List<OrderBatchStatusHistory>?> GetByOrderBatchId(Guid orderBatchId);
        // Ở đây vẫn cần GetById để phục vụ cho các lệnh khác chứ không dùng Get ngay đây để truy vấn dữ liệu
    }
}
