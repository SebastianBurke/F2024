using scbH60Store.Models;

namespace scbH60Store.DAL
{
    public interface IProductValidator
    {
        bool ValidateStock(Product product, GlobalSettings settings, out string errorMessage);
    }
}
