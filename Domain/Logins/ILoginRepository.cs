using Domain.Lines;
using Domain.Logins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSystem.Domain.Logins
{
    public interface ILoginRepository
    {
        Task AddAsync(Login login);
        Task Delete(Login login);
        Task<Login?> GetById(string loginId);
        // Ở đây vẫnLine line cần GetById để phục vụ cho các lệnh khác chứ không dùng Get ngay đây để truy vấn dữ liệu
        Task UpdateAsync(Login login);
    }
}
