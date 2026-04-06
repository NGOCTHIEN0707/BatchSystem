using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSystem.Infrastructure.Repositories
{
    public class BaseRepository
    {
        protected readonly ApplicationDbContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public BaseRepository(ApplicationDbContext context)
        {
            _context=context ?? throw new ArgumentException(nameof(context));
        }
    }
}
