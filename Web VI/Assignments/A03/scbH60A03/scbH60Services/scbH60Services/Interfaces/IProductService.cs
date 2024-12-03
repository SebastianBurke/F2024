using System.Collections.Generic;
using System.Threading.Tasks;
using scbH60Services.Models;

namespace scbH60Services.Interfaces
{
    public interface IProductService
    {
        Task<string> AddProduct(Product product);
        Task<List<Product>> GetAllProducts();
        Task<List<ProductCategory>> GetAllProductsByCategory();
        Task<List<Product>> GetProductsFilteredAndSorted(
                string partialName,
                decimal? equalTo,
                decimal? lessThan,
                decimal? greaterThan,
                string sortBy);
        Task<Product> GetProductById(int id);
        Task<string> Edit(Product product);
        Task EditStock(int productId, int stockChange);
        Task EditPrice(int productId, decimal buyPrice, decimal sellPrice);
        Task DeleteProduct(int id);
    }
}
