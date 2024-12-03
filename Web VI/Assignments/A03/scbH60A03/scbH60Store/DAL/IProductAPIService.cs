using System.Collections.Generic;
using System.Threading.Tasks;
using scbH60Services.Models;

namespace scbH60Store.DAL
{
    public interface IProductAPIService
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
