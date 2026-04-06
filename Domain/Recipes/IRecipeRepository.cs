using Domain.ProductionOrders;
using Domain.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSystem.Domain.Recipes
{
    public interface IRecipeRepository
    {
        Task AddAsync(Recipe recipe);
        Task Delete(Recipe recipe);
        Task<Recipe?> GetById(string recipeId);
        // Ở đây vẫn cần GetById để phục vụ cho các lệnh khác chứ không dùng Get ngay đây để truy vấn dữ liệu
        Task UpdateAsync(Recipe recipe);
    }
}
