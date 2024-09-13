using Microsoft.EntityFrameworkCore;
using scbH60Store.DAL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace scbH60Store.Models
{
    public class ProductService : IProductService
    {
        private readonly H60AssignmentDbContext _context;
        private readonly IGlobalSettingsService _globalSettingsService;

        public ProductService(H60AssignmentDbContext context, IGlobalSettingsService globalSettingsService)
        {
            _context = context;
            _globalSettingsService = globalSettingsService;
        }

        // Create
        public async Task<string> AddProduct(Product product)
        {
            var settings = await _globalSettingsService.GetGlobalSettingsAsync();

            // Check if product stock adheres to global settings
            if (product.Stock < settings.MinStockLimit || product.Stock > settings.MaxStockLimit)
            {
                return $"Product stock must be between {settings.MinStockLimit} and {settings.MaxStockLimit}.";
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return "Product added successfully!";
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

        public async Task<string> Edit(Product product, IFormFile imageFile)
        {
            var settings = await _globalSettingsService.GetGlobalSettingsAsync();

            // Check if product stock adheres to global settings
            if (product.Stock < settings.MinStockLimit || product.Stock > settings.MaxStockLimit)
            {
                return $"Product stock must be between {settings.MinStockLimit} and {settings.MaxStockLimit}.";
            }

            var existingProduct = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == product.ProductId);

            if (existingProduct == null)
            {
                return "Product not found.";
            }

            existingProduct.Description = product.Description;
            existingProduct.Manufacturer = product.Manufacturer;
            existingProduct.Stock = product.Stock;
            existingProduct.BuyPrice = product.BuyPrice;
            existingProduct.SellPrice = product.SellPrice;
            existingProduct.ProdCatId = product.ProdCatId;
            existingProduct.EmployeeNotes = product.EmployeeNotes;

            // Update image if a new file is uploaded
            if (imageFile != null && imageFile.Length > 0)
            {
                // Delete old image if it is not the default image
                if (existingProduct.ImageUrl != "/images/default-image.png" && !string.IsNullOrEmpty(existingProduct.ImageUrl))
                {
                    var oldImagePath = Path.Combine("wwwroot", existingProduct.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                // Save new image
                var imagePath = Path.Combine("wwwroot/images", imageFile.FileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                existingProduct.ImageUrl = $"/images/{imageFile.FileName}";
            }

            _context.Products.Update(existingProduct);
            await _context.SaveChangesAsync();
            return "Product updated successfully!";
        }
        public async Task EditStock(int productId, int stockChange)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new ArgumentException("Product not found");

            // Calculate the new stock value
            var newStock = product.Stock + stockChange;

            // Retrieve global settings
            var settings = await _globalSettingsService.GetGlobalSettingsAsync();

            // Validate the new stock value
            if (newStock < settings.MinStockLimit || newStock > settings.MaxStockLimit)
            {
                throw new ArgumentException($"Stock must be between {settings.MinStockLimit} and {settings.MaxStockLimit}.");
            }

            // Update stock with the change
            product.Stock = newStock;

            // Update and save changes
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
            if (product == null)
            {
                throw new ArgumentException("Product not found");
            }

            // Delete image file if it's not the default image
            if (product.ImageUrl != "/images/default-image.png")
            {
                var imagePath = Path.Combine("wwwroot", product.ImageUrl.TrimStart('/'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

    }
}
