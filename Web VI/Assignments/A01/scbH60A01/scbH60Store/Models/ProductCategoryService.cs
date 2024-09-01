using Microsoft.EntityFrameworkCore;

namespace scbH60Store.Models
{
    public class ProductCategoryService
    {
        private readonly H60AssignmentDbContext _context;

        public ProductCategoryService(H60AssignmentDbContext context)
        {
            _context = context;
        }

        // Create
        public async Task AddCategoryAsync(ProductCategory category)
        {
            _context.ProductCategories.Add(category);
            await _context.SaveChangesAsync();
        }

        // Read
        public async Task<List<ProductCategory>> GetAllCategoriesAsync()
        {
            return await _context.ProductCategories.ToListAsync(); //Gets a list of product categories in an asynchronous manner. 
        }

        public async Task<ProductCategory> GetCategoryByIdAsync(int id)
        {
            return await _context.ProductCategories.FindAsync(id);
        }

        // Update
        public async Task UpdateCategoryAsync(ProductCategory category)
        {
            _context.ProductCategories.Update(category);
            await _context.SaveChangesAsync();
        }

        // Delete
        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _context.ProductCategories.FindAsync(id);
            if (category == null) throw new ArgumentException("Category not found");

            _context.ProductCategories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }

}
