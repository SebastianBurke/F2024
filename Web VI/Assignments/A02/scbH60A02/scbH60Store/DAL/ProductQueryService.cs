using Newtonsoft.Json;
using scbH60Store.Models;

namespace scbH60Store.DAL
{
    public class ProductQueryService : IProductQueryService
    {
        private readonly HttpClient _httpClient;

        public ProductQueryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            var response = await _httpClient.GetAsync("http://localhost:21905/api/products");
            if (!response.IsSuccessStatusCode) return null;

            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Product>>(responseData);
        }

        public async Task<List<ProductCategory>> GetProductsByCategoryAsync()
        {
            var response = await _httpClient.GetAsync("http://localhost:21905/api/products/bycategory");
            if (!response.IsSuccessStatusCode) return null;

            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ProductCategory>>(responseData);
        }

        public async Task<Product> GetProductDetailsAsync(int productId)
        {
            var response = await _httpClient.GetAsync($"http://localhost:21905/api/products/{productId}");
            if (!response.IsSuccessStatusCode) return null;

            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Product>(responseData);
        }

        public async Task<List<Product>> FilterAndSortProductsAsync(string partialName, decimal? equalTo, decimal? lessThan, decimal? greaterThan, string sortBy)
        {
            var queryParams = new List<string>();
            if (!string.IsNullOrEmpty(partialName)) queryParams.Add($"partialName={partialName}");
            if (equalTo.HasValue) queryParams.Add($"equalTo={equalTo}");
            if (lessThan.HasValue) queryParams.Add($"lessThan={lessThan}");
            if (greaterThan.HasValue) queryParams.Add($"greaterThan={greaterThan}");
            queryParams.Add($"sortBy={sortBy}");

            var queryString = string.Join("&", queryParams);
            var response = await _httpClient.GetAsync($"http://localhost:21905/api/products/filterandsort?{queryString}");
            if (!response.IsSuccessStatusCode) return null;

            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Product>>(responseData);
        }
    }

}
