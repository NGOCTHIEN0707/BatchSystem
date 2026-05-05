using BatchSystem.Domain.Logins.StaffCodes;
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
    public class StaffCodeRepository : BaseRepository, IStaffCodeRepository
    {
        public StaffCodeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<StaffCode?> GetStaffCodeByCode(int code)
        {
            var staffCodeToGet = await _context.StaffCodes.FirstOrDefaultAsync(x => x.Code == code);
            return staffCodeToGet;
        }
    }
}
