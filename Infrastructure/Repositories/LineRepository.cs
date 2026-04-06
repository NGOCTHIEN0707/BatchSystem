using BatchSystem.Domain.Lines;
using Domain.Lines;
using Infrastructure;
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

        public Task AddAsync(Line line)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Line line)
        {
            throw new NotImplementedException();
        }

        public Task<Line?> GetById(string lineId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Line line)
        {
            throw new NotImplementedException();
        }
    }
}
