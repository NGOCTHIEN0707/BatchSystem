using Domain.Logins;
using Domain.Materials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSystem.Domain.Materials
{
    public interface IMaterialRepository
    {
        Task AddAsync(Material material);
        Task Delete(Material material);
        Task<Material?> GetById(string materialId);
        // Ở đây vẫnLine line cần GetById để phục vụ cho các lệnh khác chứ không dùng Get ngay đây để truy vấn dữ liệu
        Task UpdateAsync(Material material);
    }
}
