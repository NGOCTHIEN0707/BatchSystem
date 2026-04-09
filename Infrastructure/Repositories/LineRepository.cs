using BatchSystem.Domain.Lines;
using BatchSystem.Infrastructure.Repositories.Common;
using Domain.Lines;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSystem.Infrastructure.Repositories
{
    public class LineRepository : BaseRepository, ILineRepository
    {
        public LineRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Line line)
        {
            await _context.Lines.AddAsync(line);
        }

        public void Delete(Line line)
        {
             _context.Lines.Remove(line);
        }

        public async Task<Line?> GetById(string lineId)
        {
            var line = await _context.Lines.AsNoTracking().FirstOrDefaultAsync(x=>x.LineId==lineId);
            return line;
        }

        public async Task<bool> IsLineCodeExisted(string lineCode)
        {
            var isLineCodeExisted = await _context.Lines.AsNoTracking().AnyAsync(x => x.LineCode == lineCode);
            return isLineCodeExisted;
        }

        public async Task<bool> IsLineNameExisted(string lineName)
        {
            var isLineNameExisted = await _context.Lines.AsNoTracking().AnyAsync(x => x.LineName == lineName);
            return isLineNameExisted;
        }

        public void UpdateAsync(Line line)
        {
            var linetoUpdate =  _context.Lines.Update(line);
        }
    }
}
