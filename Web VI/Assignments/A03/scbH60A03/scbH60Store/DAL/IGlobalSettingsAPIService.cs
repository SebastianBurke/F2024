using scbH60Store.Models;
using scbH60Services.Models;

namespace scbH60Store.DAL
{
    public interface IGlobalSettingsAPIService
    {
        Task<GlobalSettings> GetGlobalSettingsAsync();
        Task<String> UpdateGlobalSettingsAsync(GlobalSettings settings);
    }
}
