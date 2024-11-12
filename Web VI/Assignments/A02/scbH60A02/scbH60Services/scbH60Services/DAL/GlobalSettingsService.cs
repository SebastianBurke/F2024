using Microsoft.EntityFrameworkCore;
using scbH60Services.Models;

namespace scbH60Services.DAL
{
    public class GlobalSettingsService : IGlobalSettingsService
    {
        private readonly H60AssignmentDbContext _context;

        public GlobalSettingsService(H60AssignmentDbContext context)
        {
            _context = context;
        }

        public async Task<GlobalSettings> GetGlobalSettingsAsync()
        {
            var settings = await _context.GlobalSettings.FindAsync(1);
            return settings;
        }

        public async Task<string> UpdateGlobalSettingsAsync(GlobalSettings settings)
        {
            if (settings.MinStockLimit < 0)
            {
                return "MinStockLimit cannot be less than zero.";
            }

            if (settings.MaxStockLimit > 1000000)
            {
                return "MaxStockLimit cannot be greater than 1,000,000.";
            }

            var existingSettings = await _context.GlobalSettings.FindAsync(1);

            var invalidProducts = await _context.Products
                .Where(p => p.Stock < settings.MinStockLimit || p.Stock > settings.MaxStockLimit)
                .ToListAsync();

            if (invalidProducts.Any())
            {
                return $"Cannot update settings. Some existing products fall outside the new limits. Please adjust their stock first.";
            }

            if (existingSettings != null)
            {
                existingSettings.MinStockLimit = settings.MinStockLimit;
                existingSettings.MaxStockLimit = settings.MaxStockLimit;
                _context.Update(existingSettings);
            }
            else
            {
                _context.GlobalSettings.Add(settings);
            }

            await _context.SaveChangesAsync();
            return "Settings updated successfully!";
        }
    }
}
