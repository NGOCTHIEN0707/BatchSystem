using BatchSystem.Domain.OrderBatchs;
using BatchSystem.Domain.OrderBatchs.OrderBatchStatusHistories;
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
    public class OrderBatchStatusHistoryRepository : BaseRepository, IOrderBatchStatusHistoryRepository
    {
        public OrderBatchStatusHistoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task AddAsync(OrderBatchStatusHistory orderBatchStatusHistory)
        {
            await _context.OrderBatchStatusHistories.AddAsync(orderBatchStatusHistory);
        }

        public void Delete(OrderBatchStatusHistory orderBatchStatusHistory)
        {
            _context.Remove(orderBatchStatusHistory);
        }

        public async Task<List<OrderBatchStatusHistory>?> GetByOrderBatchId(Guid orderBatchId)
        {
            var orderBatchHistoryToGet = await _context.OrderBatchStatusHistories.AsNoTracking().Where(x=>x.OrderBatch.OrderBatchId == orderBatchId)
                .ToListAsync();
            return orderBatchHistoryToGet;  
        }
    }
}
