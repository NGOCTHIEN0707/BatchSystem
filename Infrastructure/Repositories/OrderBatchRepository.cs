using BatchSystem.Domain.OrderBatchs;
using BatchSystem.Infrastructure.Repositories.Common;
using Domain.OrderBatchs;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSystem.Infrastructure.Repositories
{
    public class OrderBatchRepository : BaseRepository, IOrderBatchRepository
    {
        public OrderBatchRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task AddAsync(OrderBatch orderBatch)
        {
            await _context.OrderBatchs.AddAsync(orderBatch);
        }

        public void Delete(OrderBatch orderBatch)
        {
            _context.OrderBatchs.Remove(orderBatch);
        }

        public async Task<OrderBatch?> GetById(Guid orderBatchId)
        {
            var orderBatch = await _context.OrderBatchs.AsNoTracking()
                .Include(x=>x.ProductionOrderDetail)
                .FirstOrDefaultAsync(x => x.OrderBatchId == orderBatchId);
            return orderBatch;
        }

        public void UpdateAsync(OrderBatch orderBatch)
        {
            _context.OrderBatchs.Update(orderBatch);
        }
    }
}
