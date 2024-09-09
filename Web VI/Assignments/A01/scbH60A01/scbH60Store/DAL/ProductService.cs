using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace scbH60Store.Models
{
    public class ProductService : IProductService
    {
        private readonly H60AssignmentDbContext _context;

        public ProductService(H60AssignmentDbContext context)
        {
            _context = context;
        }

        public async Task AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.Include(p => p.ProdCat)
                .FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task Edit(Product product)
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

        public async Task EditStock(int productId, int newStock)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new ArgumentException("Product not found");

            product.Stock = newStock;
            if (product.Stock < 0) throw new ArgumentException("Stock cannot be negative");

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task EditPrice(int productId, decimal buyPrice, decimal sellPrice)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new ArgumentException("Product not found");

            if (buyPrice < 0 || sellPrice < 0) throw new ArgumentException("Price cannot be negative");
            if (sellPrice < buyPrice) throw new ArgumentException("Sell price cannot be less than buy price");

            product.BuyPrice = buyPrice;
            product.SellPrice = sellPrice;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) throw new ArgumentException("Product not found");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
