using System.Collections.Generic;
using System.Threading.Tasks;

namespace scbH60Store.Models
{
    public interface IProductCategoryService
    {
        Task AddCategory(ProductCategory category);
        Task<List<ProductCategory>> GetAllCategories();
        Task<ProductCategory> GetCategoryById(int id);
        Task UpdateCategory(ProductCategory category);
        Task DeleteCategory(int id);
    }
}
