using BatchSystem.Domain.Materials;
using Domain.Materials;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSystem.Infrastructure.Repositories
{
    public class MaterialRepository : BaseRepository, IMaterialRepository
    {
        public MaterialRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task AddAsync(Material material)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Material material)
        {
            throw new NotImplementedException();
        }

        public Task<Material?> GetById(string materialId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Material material)
        {
            throw new NotImplementedException();
        }
    }
}
