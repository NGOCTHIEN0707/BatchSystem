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
            var productionOrder = await _context.ProductionOrders.AsNoTracking().FirstOrDefaultAsync(x=>x.ProductionOrderId == productionOrderId);
            return productionOrder;
        }

        public void UpdateAsync(ProductionOrder productionOrder)
        {
            _context.ProductionOrders.Update(productionOrder);
        }
       
    }
}
