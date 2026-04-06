using BatchSystem.Domain.ProductionOrders;
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

        public Task AddAsync(ProductionOrder productionOrder)
        {
            throw new NotImplementedException();
        }

        public Task Delete(ProductionOrder productionOrder)
        {
            throw new NotImplementedException();
        }

        public Task<ProductionOrder?> GetById(Guid productionOrderId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ProductionOrder productionOrder)
        {
            throw new NotImplementedException();
        }
    }
}
