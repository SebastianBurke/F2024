using System.Collections.Generic;
using System.Threading.Tasks;
using scbH60Services.Models;

namespace scbH60Services.Interfaces
{
    public interface IProductCategoryService
    {
        Task AddCategory(ProductCategory category);
        Task<List<ProductCategory>> GetAllCategories();
        Task<List<Product>> GetCategoryProducts(int id);
        Task<ProductCategory> GetCategoryById(int id);
        Task UpdateCategory(ProductCategory category);
        Task DeleteCategory(int id);
    }
}
