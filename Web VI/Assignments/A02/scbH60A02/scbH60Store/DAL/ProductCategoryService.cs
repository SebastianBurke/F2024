using Newtonsoft.Json;
using System.Text;
using static scbH60Store.Controllers.ProductCategoryController;

namespace scbH60Store.Models
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly HttpClient _httpClient;

        public ProductCategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductCategory>> GetAllCategoriesAsync()
        {
            var response = await _httpClient.GetAsync("http://localhost:21905/api/productcategory");
            if (!response.IsSuccessStatusCode) return null;

            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ProductCategory>>(responseData);
        }

        public async Task<CategoryProductsResponse> GetProductsByCategoryAsync(int categoryId)
        {
            var response = await _httpClient.GetAsync($"http://localhost:21905/api/productcategory/{categoryId}/products");
            if (!response.IsSuccessStatusCode) return null;

            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CategoryProductsResponse>(responseData);
        }

        public async Task<ProductCategory> GetCategoryByIdAsync(int categoryId)
        {
            var response = await _httpClient.GetAsync($"http://localhost:21905/api/productcategory/{categoryId}");
            if (!response.IsSuccessStatusCode) return null;

            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ProductCategory>(responseData);
        }

        public async Task<HttpResponseMessage> CreateCategoryAsync(ProductCategory category)
        {
            var categoryContent = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync("http://localhost:21905/api/productcategory", categoryContent);
        }

        public async Task<HttpResponseMessage> UpdateCategoryAsync(ProductCategory category)
        {
            var categoryContent = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");
            return await _httpClient.PutAsync($"http://localhost:21905/api/productcategory/{category.CategoryId}", categoryContent);
        }

        public async Task<HttpResponseMessage> DeleteCategoryAsync(int categoryId)
        {
            return await _httpClient.DeleteAsync($"http://localhost:21905/api/productcategory/{categoryId}");
        }
    }
}
