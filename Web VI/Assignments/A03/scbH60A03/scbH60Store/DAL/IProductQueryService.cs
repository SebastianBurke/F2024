using scbH60Store.Models;
using scbH60Services.Models;

namespace scbH60Store.DAL
{
    public interface IProductQueryService
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<List<ProductCategory>> GetProductsByCategoryAsync();
        Task<Product> GetProductDetailsAsync(int productId);
        Task<List<Product>> FilterAndSortProductsAsync(string partialName, decimal? equalTo, decimal? lessThan, decimal? greaterThan, string sortBy);
    }
}
