using static scbH60Store.Controllers.ProductCategoryController;

namespace scbH60Store.Models
{
    public interface IProductCategoryService
    {
        Task<List<ProductCategory>> GetAllCategoriesAsync();
        Task<CategoryProductsResponse> GetProductsByCategoryAsync(int categoryId);
        Task<ProductCategory> GetCategoryByIdAsync(int categoryId);
        Task<HttpResponseMessage> CreateCategoryAsync(ProductCategory category);
        Task<HttpResponseMessage> UpdateCategoryAsync(ProductCategory category);
        Task<HttpResponseMessage> DeleteCategoryAsync(int categoryId);
    }
}
