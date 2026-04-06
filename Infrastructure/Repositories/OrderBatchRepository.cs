using BatchSystem.Domain.OrderBatchs;
using Domain.OrderBatchs;
using Infrastructure;
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

        public Task AddAsync(OrderBatch orderBatch)
        {
            throw new NotImplementedException();
        }

        public Task Delete(OrderBatch orderBatch)
        {
            throw new NotImplementedException();
        }

        public Task<OrderBatch?> GetById(Guid orderBatchId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(OrderBatch orderBatch)
        {
            throw new NotImplementedException();
        }
    }
}
