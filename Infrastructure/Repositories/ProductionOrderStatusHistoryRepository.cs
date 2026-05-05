using BatchSystem.Domain.ProductionOrders.ProductionOrderStatusHistories;
using BatchSystem.Infrastructure.Repositories.Common;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSystem.Infrastructure.Repositories
{
    public class ProductionOrderStatusHistoryRepository : BaseRepository, IProductionOrderStatusHistoryRepository
    {
        public ProductionOrderStatusHistoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task AddAsync(ProductionOrderStatusHistory history)
        {
           await _context.ProductionOrderStatusHistories.AddAsync(history);
        }

        public async Task<List<ProductionOrderStatusHistory>?> GetByProductionOrderId(Guid productionOrderId)
        {
            var productionOrderHistoryToGet = await _context.ProductionOrderStatusHistories.AsNoTracking().
                Where(x=>x.ProductionOrderId == productionOrderId).ToListAsync();
            return productionOrderHistoryToGet;
        }
    }
}
