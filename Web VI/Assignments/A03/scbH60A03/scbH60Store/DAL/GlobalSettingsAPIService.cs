using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using scbH60Store.Models;
using System.Text;
using scbH60Services.Models;

namespace scbH60Store.DAL
{
    public class GlobalSettingsAPIService : IGlobalSettingsAPIService
    {
        private readonly HttpClient _httpClient;

        public GlobalSettingsAPIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GlobalSettings> GetGlobalSettingsAsync()
        {
            var response = await _httpClient.GetAsync("http://localhost:21905/api/globalsettings");
            if (!response.IsSuccessStatusCode) return null;

            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GlobalSettings>(responseData);
        }

        public async Task<string> UpdateGlobalSettingsAsync(GlobalSettings settings)
        {
            var content = new StringContent(JsonConvert.SerializeObject(settings), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("http://localhost:21905/api/globalsettings", content);

            return await response.Content.ReadAsStringAsync();
        }

    }


}
