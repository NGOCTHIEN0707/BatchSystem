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
        void Delete(Material material);
        Task<bool> IsMaterialNameExisted(string materialName);
        Task<Material?> GetById(string materialId);
        // Ở đây vẫnLine line cần GetById để phục vụ cho các lệnh khác chứ không dùng Get ngay đây để truy vấn dữ liệu
        void UpdateAsync(Material material);
    }
}
