using Domain.ProductionOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSystem.Domain.ProductionOrders
{
    public interface IProductionOrderRepository
    {
        Task AddAsync(ProductionOrder productionOrder);
        Task Delete(ProductionOrder productionOrder);
        Task<ProductionOrder?> GetById(Guid productionOrderId);
        // Ở đây vẫn cần GetById để phục vụ cho các lệnh khác chứ không dùng Get ngay đây để truy vấn dữ liệu
        Task UpdateAsync(ProductionOrder productionOrder);


    }
}
