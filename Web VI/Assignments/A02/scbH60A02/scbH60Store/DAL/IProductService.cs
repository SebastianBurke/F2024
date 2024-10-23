using System.Collections.Generic;
using System.Threading.Tasks;

namespace scbH60Store.Models
{
    public interface IProductService
    {
        Task<List<ProductCategory>> GetAllProductsByCategoryAsync();
        Task<List<ProductCategory>> GetProductCategoriesAsync();
        Task<GlobalSettings> GetGlobalSettingsAsync();
        Task<HttpResponseMessage> CreateProductAsync(Product product);
        Task<Product> GetProductByIdAsync(int productId);
        Task<HttpResponseMessage> UpdateProductAsync(Product product);
        Task<HttpResponseMessage> UpdateProductStockAsync(int productId, int stockChange);
        Task<HttpResponseMessage> UpdateProductPriceAsync(int productId, decimal? buyPrice, decimal? sellPrice);
        Task<HttpResponseMessage> DeleteProductAsync(int productId);
    }
}
