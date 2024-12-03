using scbH60Services.Models;

namespace scbH60Services.Interfaces
{
    public interface IGlobalSettingsService
    {
        Task<GlobalSettings> GetGlobalSettingsAsync();
        Task<string> UpdateGlobalSettingsAsync(GlobalSettings settings);
    }
}
