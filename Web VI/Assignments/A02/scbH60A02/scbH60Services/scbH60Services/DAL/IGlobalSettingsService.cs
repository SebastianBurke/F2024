using scbH60Services.Models;

namespace scbH60Services.DAL
{
    public interface IGlobalSettingsService
    {
        Task<GlobalSettings> GetGlobalSettingsAsync();
        Task<string> UpdateGlobalSettingsAsync(GlobalSettings settings);
    }
}
