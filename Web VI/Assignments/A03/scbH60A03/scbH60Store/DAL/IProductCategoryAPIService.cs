using static scbH60Store.Controllers.ProductCategoryController;
using scbH60Services.Models;

namespace scbH60Store.DAL
{
    public interface IProductCategoryAPIService
    {
        Task<List<ProductCategory>> GetAllCategoriesAsync();
        Task<CategoryProductsResponse> GetProductsByCategoryAsync(int categoryId);
        Task<ProductCategory> GetCategoryByIdAsync(int categoryId);
        Task<HttpResponseMessage> CreateCategoryAsync(ProductCategory category);
        Task<HttpResponseMessage> UpdateCategoryAsync(ProductCategory category);
        Task<HttpResponseMessage> DeleteCategoryAsync(int categoryId);
    }
}
