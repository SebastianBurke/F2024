using scbH60Store.Models;

namespace scbH60Store.DAL
{
    public interface IGlobalSettingsService
    {
        Task<GlobalSettings> GetGlobalSettingsAsync();
        Task<String> UpdateGlobalSettingsAsync(GlobalSettings settings);
    }
}
