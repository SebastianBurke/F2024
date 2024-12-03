using scbH60Services.Models;

namespace scbH60Store.DAL
{
    public class ProductValidator : IProductValidator
    {
        public bool ValidateStock(Product product, GlobalSettings settings, out string errorMessage)
        {
            errorMessage = string.Empty;
            if (product.Stock < settings.MinStockLimit || product.Stock > settings.MaxStockLimit)
            {
                errorMessage = $"Stock must be between {settings.MinStockLimit} and {settings.MaxStockLimit}.";
                return false;
            }
            return true;
        }
    }
}
