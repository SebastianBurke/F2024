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

        // Create
        public async Task AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        // Read

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.OrderBy(p => p.Description).ToListAsync();
        }

        public async Task<List<ProductCategory>> GetAllProductsByCategory()
        {
            return await _context.ProductCategories.Include(p => p.Products).ToListAsync();
        }

        public async Task<List<Product>> GetProductsFilteredAndSorted(
            string partialName,
            decimal? equalTo = null,
            decimal? lessThan = null,
            decimal? greaterThan = null,
            string sortBy = "description")
        {
            var query = _context.Products.AsQueryable();

            // Apply partial name filter
            if (!string.IsNullOrWhiteSpace(partialName))
            {
                query = query.Where(p => p.Description.Contains(partialName));
            }

            // Apply price filters
            if (equalTo.HasValue)
            {
                query = query.Where(p => p.SellPrice == equalTo.Value);
            }

            if (lessThan.HasValue)
            {
                query = query.Where(p => p.SellPrice < lessThan.Value);
            }

            if (greaterThan.HasValue)
            {
                query = query.Where(p => p.SellPrice > greaterThan.Value);
            }

            // Apply sorting
            query = sortBy.ToLower() switch
            {
                "price" => query.OrderBy(p => p.SellPrice ?? 0),
                "stock" => query.OrderBy(p => p.Stock),
                "markup" => query.OrderBy(p => (p.SellPrice ?? 0) - (p.BuyPrice ?? 0)),
                _ => query.OrderBy(p => p.Description),
            };

            return await query.ToListAsync();
        }


        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.Include(p => p.ProdCat)
                .FirstOrDefaultAsync(p => p.ProductId == id);
        }

        // Update

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

            // Ensure the ImageUrl is updated only if a new file was uploaded
            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                existingProduct.ImageUrl = product.ImageUrl;
            }

            _context.Products.Update(existingProduct);
            await _context.SaveChangesAsync();
        }


        public async Task EditStock(int productId, int stockChange)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new ArgumentException("Product not found");

            product.Stock += stockChange;
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

            // Round prices to 2 decimal places
            buyPrice = Math.Round(buyPrice, 2, MidpointRounding.AwayFromZero);
            sellPrice = Math.Round(sellPrice, 2, MidpointRounding.AwayFromZero);

            product.BuyPrice = buyPrice;
            product.SellPrice = sellPrice;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        // Delete

        public async Task DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) throw new ArgumentException("Product not found");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
