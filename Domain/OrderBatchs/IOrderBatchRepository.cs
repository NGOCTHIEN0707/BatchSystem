using Domain.Lines;
using Domain.OrderBatchs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSystem.Domain.OrderBatchs
{
    public interface IOrderBatchRepository
    {
        Task AddAsync(OrderBatch orderBatch);
        void Delete(OrderBatch orderBatch);
        Task<OrderBatch?> GetById(Guid orderBatchId);
        // Ở đây vẫn cần GetById để phục vụ cho các lệnh khác chứ không dùng Get ngay đây để truy vấn dữ liệu
        void UpdateAsync(OrderBatch orderBatch);
    }
}
