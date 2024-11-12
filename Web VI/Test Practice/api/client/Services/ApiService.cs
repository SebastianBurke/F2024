using client.DTOs;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using client.Models;

namespace client.Services
{

    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PersonDTO>> GetAllPersonsAsync(string? searchTerm = null)
        {
            string url = "api/person";
            if (!string.IsNullOrEmpty(searchTerm))
            {
                url += $"?searchTerm={searchTerm}";
            }
            return await _httpClient.GetFromJsonAsync<List<PersonDTO>>(url);
        }


        public async Task<PersonDTO> GetPersonByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<PersonDTO>($"api/person/{id}");
        }

        public async Task AddPersonAsync(PersonDTO person)
        {
            await _httpClient.PostAsJsonAsync("api/person", person);
        }

        public async Task UpdatePersonAsync(int id, PersonDTO person)
        {
            await _httpClient.PutAsJsonAsync($"api/person/{id}", person);
        }

        public async Task DeletePersonAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/person/{id}");
        }
        public async Task<List<PersonDTO>> GetPersonsWithPetsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PersonDTO>>("api/person/withpets");
        }
    }

}
