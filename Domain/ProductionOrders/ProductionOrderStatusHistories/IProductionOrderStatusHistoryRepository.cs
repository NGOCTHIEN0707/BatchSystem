using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSystem.Domain.ProductionOrders.ProductionOrderStatusHistories
{
    public interface IProductionOrderStatusHistoryRepository
    {
        Task AddAsync(ProductionOrderStatusHistory history);
        Task<List<ProductionOrderStatusHistory>?> GetByProductionOrderId(Guid productionOrderId);
    }
}
