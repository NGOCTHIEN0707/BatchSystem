using BatchSystem.Domain.Logins;
using Domain.Logins;
using Infrastructure;
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

        public Task AddAsync(Login login)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Login login)
        {
            throw new NotImplementedException();
        }

        public Task<Login?> GetById(string loginId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Login login)
        {
            throw new NotImplementedException();
        }
    }
}
