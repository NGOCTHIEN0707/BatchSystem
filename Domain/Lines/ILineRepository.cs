using Domain.Lines;
using Domain.ProductionOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSystem.Domain.Lines
{
    public interface ILineRepository
    {
        Task AddAsync(Line line);
        Task Delete(Line line);
        Task<Line?> GetById(string lineId);
        // Ở đây vẫn cần GetById để phục vụ cho các lệnh khác chứ không dùng Get ngay đây để truy vấn dữ liệu
        Task UpdateAsync(Line line);
    }
}
