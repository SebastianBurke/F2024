using Microsoft.EntityFrameworkCore;
using scbH60Store.DAL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace scbH60Store.Models
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly H60AssignmentDbContext _context;
        private readonly IGlobalSettingsService _globalSettingsService;


        public ProductCategoryService(H60AssignmentDbContext context, IGlobalSettingsService globalSettingsService)
        {
            _context = context;
            _globalSettingsService = globalSettingsService;
        }

        public async Task AddCategory(ProductCategory category)
        {
            _context.ProductCategories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ProductCategory>> GetAllCategories()
        {
            return await _context.ProductCategories.ToListAsync();
        }

        public async Task<ProductCategory> GetCategoryById(int id)
        {
            return await _context.ProductCategories.FindAsync(id);
        }
        public async Task<List<Product>> GetCategoryProducts(int id)
        {
            return await _context.Products
                       .Where(p => p.ProdCatId == id)
                       .ToListAsync();
        }

        public async Task UpdateCategory(ProductCategory category)
        {
            _context.ProductCategories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategory(int id)
        {
            var category = await _context.ProductCategories
                                         .Include(c => c.Products)
                                         .FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null)
            {
                throw new ArgumentException("Category not found");
            }

            if (category.Products != null && category.Products.Any())
            {
                foreach (var product in category.Products)
                {
                    // Delete image file if it's not the default image
                    if (product.ImageUrl != "/images/default-image.png")
                    {
                        var imagePath = Path.Combine("wwwroot", product.ImageUrl.TrimStart('/'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                }

                // Remove all products associated with the category
                _context.Products.RemoveRange(category.Products);
            }

            // Remove the category itself
            _context.ProductCategories.Remove(category);

            await _context.SaveChangesAsync();
        }


    }
}
