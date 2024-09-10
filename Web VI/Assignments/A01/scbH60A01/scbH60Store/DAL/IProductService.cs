using System.Collections.Generic;
using System.Threading.Tasks;

namespace scbH60Store.Models
{
    public interface IProductService
    {
        Task AddProduct(Product product);
        Task<List<Product>> GetAllProducts();
        Task<List<ProductCategory>> GetAllProductsByCategory();
        Task<List<Product>> GetProductsByPartialName(string partialName);
        Task<List<Product>> GetProductsByPrice(decimal? equalTo, decimal? lessThan, decimal? greaterThan);
        Task<List<Product>> GetProductsSorted(string sortBy, bool ascending);
        Task<Product> GetProductById(int id);
        Task Edit(Product product);
        Task EditStock(int productId, int stockChange);
        Task EditPrice(int productId, decimal buyPrice, decimal sellPrice);
        Task DeleteProduct(int id);
    }
}
