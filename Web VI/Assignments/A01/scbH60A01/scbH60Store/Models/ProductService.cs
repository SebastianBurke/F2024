using Microsoft.EntityFrameworkCore;

namespace scbH60Store.Models
{
    public class ProductService
    {
        private readonly H60AssignmentDbContext _context;

        public ProductService(H60AssignmentDbContext context)
        {
            _context = context;
        }

        // Create
        public async Task AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        // Read
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        // Update
        public async Task UpdateProductAsync(Product product)
        {
            var existingProduct = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == product.ProductId);

            if (existingProduct == null) throw new ArgumentException("Product not found");

            existingProduct.Description = product.Description;
            existingProduct.Manufacturer = product.Manufacturer;
            existingProduct.Stock = product.Stock;
            existingProduct.BuyPrice = product.BuyPrice;
            existingProduct.SellPrice = product.SellPrice;
            existingProduct.ProdCatId = product.ProdCatId;

            _context.Products.Update(existingProduct);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStockAsync(int productId, int stockChange)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new ArgumentException("Product not found");

            int newStock = product.Stock + stockChange;
            if (newStock < 0) throw new ArgumentException("Stock cannot be negative");

            product.Stock = newStock;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePriceAsync(int productId, decimal buyPrice, decimal sellPrice)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new ArgumentException("Product not found");

            if (buyPrice < 0 || sellPrice < 0) throw new ArgumentException("Price cannot be negative");
            if (sellPrice < buyPrice) throw new ArgumentException("Sell price cannot be less than buy price");

            product.BuyPrice = Math.Round(buyPrice, 2);
            product.SellPrice = Math.Round(sellPrice, 2);

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        // Delete
        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) throw new ArgumentException("Product not found");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
