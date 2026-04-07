using BatchSystem.Domain.Logins;
using BatchSystem.Infrastructure.Repositories.Common;
using Domain.Logins;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSystem.Infrastructure.Repositories
{
    public class LoginRepository : BaseRepository, ILoginRepository
    {
        public LoginRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Login login)
        {
            await _context.Logins.AddAsync(login);
        }

        public void Delete(Login login)
        {
            _context.Logins.Remove(login);
        }

        public async Task<Login?> GetById(string loginId)
        {
            var user = await _context.Logins.AsNoTracking().FirstOrDefaultAsync(x=>x.LoginId == loginId);
            return user;
        }

        public void UpdateAsync(Login login)
        {
            _context.Logins.Update(login);
        }
    }
}
