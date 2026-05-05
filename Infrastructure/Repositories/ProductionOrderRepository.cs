using BatchSystem.Domain.ProductionOrders;
using BatchSystem.Infrastructure.Repositories.Common;
using Domain.ProductionOrders;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSystem.Infrastructure.Repositories
{
    public class ProductionOrderRepository : BaseRepository, IProductionOrderRepository
    {
        public ProductionOrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task AddAsync(ProductionOrder productionOrder)
        {
            await _context.ProductionOrders.AddAsync(productionOrder);
        }

        public void Delete(ProductionOrder productionOrder)
        {
            _context.ProductionOrders.Remove(productionOrder);
        }

        public async Task<ProductionOrder?> GetById(Guid productionOrderId)
        {
            var productionOrder = await _context.ProductionOrders.AsNoTracking()
                .Include(x=>x.ProductionOrderDetails)
                .ThenInclude(x=>x.OrderBatches)
                .FirstOrDefaultAsync(x => x.ProductionOrderId == productionOrderId);
            return productionOrder;
        }

        public async Task<ProductionOrder?> GetByIdTracking(Guid productionOrderId)
        {
            var productionOrder = await _context.ProductionOrders
                .Include(x => x.ProductionOrderDetails)
                .ThenInclude(x => x.OrderBatches)
                .FirstOrDefaultAsync(x => x.ProductionOrderId == productionOrderId);
            return productionOrder;
        }

        public void UpdateAsync(ProductionOrder productionOrder)
        {
            _context.ProductionOrders.Update(productionOrder);
        }
        public async Task<bool> HasRunningOrder()
        {
            return await _context.ProductionOrders
                .AnyAsync(x => x.Status == ProductionOrderStatus.Running);
        }

        public async Task<ProductionOrder?> GetNextReadyOrderTracking()
        {
            return await _context.ProductionOrders
                .Include(x => x.ProductionOrderDetails)
                    .ThenInclude(d => d.OrderBatches)
                .Where(x => x.Status == ProductionOrderStatus.Ready)
                .OrderBy(x => x.Priority)// Mức độ ưu tiên của Đơn hàng 
                .ThenBy(x => x.PlannedStartTime)
                .ThenBy(x => x.CreatedAt)
                .FirstOrDefaultAsync();
        }

    }
}
