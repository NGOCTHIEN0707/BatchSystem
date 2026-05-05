using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSystem.Domain.Logins.StaffCodes
{
    public interface IStaffCodeRepository
    {
        Task<StaffCode?> GetStaffCodeByCode(int code);
    }
}
