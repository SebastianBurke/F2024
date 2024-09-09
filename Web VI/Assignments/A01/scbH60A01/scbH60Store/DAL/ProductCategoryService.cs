using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace scbH60Store.Models
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly H60AssignmentDbContext _context;

        public ProductCategoryService(H60AssignmentDbContext context)
        {
            _context = context;
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
            var category = await _context.ProductCategories.FindAsync(id);
            if (category == null) throw new ArgumentException("Category not found");

            _context.ProductCategories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
