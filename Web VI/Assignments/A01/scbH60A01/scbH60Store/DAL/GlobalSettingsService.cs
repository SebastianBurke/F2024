using Microsoft.EntityFrameworkCore;
using scbH60Store.Models;

namespace scbH60Store.DAL
{
    public class GlobalSettingsService : IGlobalSettingsService
    {
        private readonly H60AssignmentDbContext _context;

        public GlobalSettingsService(H60AssignmentDbContext context)
        {
            _context = context;
        }

        // Read
        public async Task<GlobalSettings> GetGlobalSettingsAsync()
        {
            var settings = await _context.GlobalSettings.FindAsync(1);
            if (settings == null)
            {
                // Handle case where settings do not exist
                settings = new GlobalSettings { Id = 1, MinStockLimit = 0, MaxStockLimit = 1000 }; // Example default values
            }
            return settings;
        }

        // Update
        public async Task<string> UpdateGlobalSettingsAsync(GlobalSettings settings)
        {
            var existingSettings = await _context.GlobalSettings.FindAsync(1);
            if (existingSettings != null)
            {
                // Check if any product's stock is outside the new limits
                var invalidProducts = await _context.Products
                    .Where(p => p.Stock < settings.MinStockLimit || p.Stock > settings.MaxStockLimit)
                    .ToListAsync();

                if (invalidProducts.Any())
                {
                    // Return an error message if there are invalid products
                    return "Cannot update settings. Please adjust the stock of the products that fall outside the new limits.";
                }

                existingSettings.MinStockLimit = settings.MinStockLimit;
                existingSettings.MaxStockLimit = settings.MaxStockLimit;
                _context.Update(existingSettings);
                await _context.SaveChangesAsync();
                return "Settings updated successfully!";
            }
            else
            {
                // Handle case where settings do not exist
                _context.GlobalSettings.Add(settings);
                await _context.SaveChangesAsync();
                return "Settings added successfully!";
            }
        }

    }


}
