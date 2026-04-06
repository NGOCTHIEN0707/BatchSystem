using Domain.ProductionOrders;
using Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSystem.Domain.Products
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);
        Task Delete(Product product);
        Task<Product?> GetById(string productId);
        // Ở đây vẫn cần GetById để phục vụ cho các lệnh khác chứ không dùng Get ngay đây để truy vấn dữ liệu
        Task UpdateAsync(Product product);
    }
}
