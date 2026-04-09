using BatchSystem.Domain.Products;
using BatchSystem.Infrastructure.Repositories.Common;
using Domain.Products;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSystem.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public void Delete(Product product)
        {
            _context.Products.Remove(product);
        }

        public async Task<Product?> GetById(string productId)
        {
            var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x=> x.ProductId == productId);
            return product;
        }

        public async Task<bool> IsProductNameExisted(string productName)
        {
            var productNameCheck = await _context.Products.AnyAsync(x=> x.ProductName == productName);
            return productNameCheck;
        }

        public void UpdateAsync(Product product)
        {
            _context.Products.Update(product);
        }
    }
}
