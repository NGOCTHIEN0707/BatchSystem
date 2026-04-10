using BatchSystem.Domain.Materials;
using BatchSystem.Infrastructure.Repositories.Common;
using Domain.Materials;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
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

        public async Task AddAsync(Material material)
        {
            await _context.Materials.AddAsync(material);
        }

        public void Delete(Material material)
        {
            _context.Materials.Remove(material);
        }

        public async Task<Material?> GetById(string materialId)
        {
            var material = await _context.Materials.AsNoTracking().FirstOrDefaultAsync(x=>x.MaterialId == materialId);
            return material;
        }

        public async Task<bool> IsMaterialNameExisted(string materialName)
        {
            var isMaterialNameExisted = await _context.Materials.AsNoTracking().AnyAsync(x=>x.MaterialName == materialName);
            return isMaterialNameExisted;
        }

        public void UpdateAsync(Material material)
        {
            _context.Materials.Update(material);
        }
    }
}
