using System.Collections.Generic;
using System.Threading.Tasks;

namespace scbH60Store.Models
{
    public interface IProductService
    {
        Task AddProduct(Product product);
        Task<List<Product>> GetAllProducts();
        Task<List<ProductCategory>> GetAllProductsByCategory();
        Task<Product> GetProductById(int id);
        Task Edit(Product product);
        Task EditStock(int productId, int stockChange);
        Task EditPrice(int productId, decimal buyPrice, decimal sellPrice);
        Task DeleteProduct(int id);
    }
}
