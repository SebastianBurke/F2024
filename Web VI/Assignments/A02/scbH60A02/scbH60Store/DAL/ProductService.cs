// ProductService.cs
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using scbH60Store.Models;
using System.Text;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ProductCategory>> GetAllProductsByCategoryAsync()
    {
        var response = await _httpClient.GetAsync("http://localhost:21905/api/products/bycategory");

        if (!response.IsSuccessStatusCode) return null;

        var responseData = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<ProductCategory>>(responseData);
    }

    public async Task<List<ProductCategory>> GetProductCategoriesAsync()
    {
        var response = await _httpClient.GetAsync("http://localhost:21905/api/products/categories");
        response.EnsureSuccessStatusCode();

        var responseData = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<ProductCategory>>(responseData);
    }

    public async Task<GlobalSettings> GetGlobalSettingsAsync()
    {
        var response = await _httpClient.GetAsync("http://localhost:21905/api/globalsettings");
        response.EnsureSuccessStatusCode();

        var responseData = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<GlobalSettings>(responseData);
    }

    public async Task<HttpResponseMessage> CreateProductAsync(Product product)
    {
        var productContent = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
        return await _httpClient.PostAsync("http://localhost:21905/api/products", productContent);
    }

    public async Task<Product> GetProductByIdAsync(int productId)
    {
        var response = await _httpClient.GetAsync($"http://localhost:21905/api/products/{productId}");
        if (!response.IsSuccessStatusCode) return null;

        var productData = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Product>(productData);
    }

    public async Task<HttpResponseMessage> UpdateProductAsync(Product product)
    {
        var productContent = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
        return await _httpClient.PutAsync($"http://localhost:21905/api/products/{product.ProductId}", productContent);
    }

    public async Task<HttpResponseMessage> UpdateProductStockAsync(int productId, int stockChange)
    {
        var content = new StringContent(stockChange.ToString(), Encoding.UTF8, "application/json");
        return await _httpClient.PostAsync($"http://localhost:21905/api/products/editstock/{productId}", content);
    }

    public async Task<HttpResponseMessage> UpdateProductPriceAsync(int productId, decimal? buyPrice, decimal? sellPrice)
    {
        var productPriceUpdate = new
        {
            BuyPrice = buyPrice,
            SellPrice = sellPrice
        };

        var content = new StringContent(JsonConvert.SerializeObject(productPriceUpdate), Encoding.UTF8, "application/json");
        return await _httpClient.PutAsync($"http://localhost:21905/api/products/{productId}/price", content);
    }

    public async Task<HttpResponseMessage> DeleteProductAsync(int productId)
    {
        return await _httpClient.DeleteAsync($"http://localhost:21905/api/products/{productId}");
    }
}
