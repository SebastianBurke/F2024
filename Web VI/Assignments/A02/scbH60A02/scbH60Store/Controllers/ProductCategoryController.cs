using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using scbH60Store.Models;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace scbH60Store.Controllers
{
    [Authorize(Roles = "Clerk,Manager")]
    public class ProductCategoryController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductCategoryController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCategory category, IFormFile imageFile)
        {
            if (ModelState.ContainsKey("ImageFile"))
            {
                ModelState.Remove("ImageFile");
            }
            if (ModelState.ContainsKey("ImageUrl"))
            {
                ModelState.Remove("ImageUrl");
            }
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    var imagePath = Path.Combine("wwwroot/images", imageFile.FileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    category.ImageUrl = $"/images/{imageFile.FileName}";
                }
                else
                {
                    category.ImageUrl = "/images/default-image.png";
                }

                var categoryContent = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");
                var apiResponse = await _httpClient.PostAsync("http://localhost:21905/api/productcategory", categoryContent);

                if (apiResponse.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                var resultMessage = await apiResponse.Content.ReadAsStringAsync();
                ModelState.AddModelError("", resultMessage);
            }
            return View(category);
        }



        // Read
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("http://localhost:21905/api/productcategory");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<List<ProductCategory>>(responseData);

            return View(categories);
        }


        [HttpGet]
        public async Task<IActionResult> CategoryProducts(int id)
        {
            // Make an API call to get products by category ID
            var response = await _httpClient.GetAsync($"http://localhost:21905/api/productcategory/{id}/products");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            // Deserialize the response
            var responseData = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CategoryProductsResponse>(responseData);

            if (result == null || result.Products == null)
            {
                return View("Error"); // Return error if there's a problem with the API response
            }

            // Set the category name in ViewBag
            ViewBag.CategoryName = result.CategoryName ?? "Unknown Category";
            ViewBag.CategoryId = id;

            // Pass the products list to the view
            return View(result.Products);
        }

        // Create a matching model for the deserialization
        public class CategoryProductsResponse
        {
            public string CategoryName { get; set; }
            public List<Product> Products { get; set; }
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int categoryId)
        {
            var response = await _httpClient.GetAsync($"http://localhost:21905/api/productcategory/{categoryId}");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var category = JsonConvert.DeserializeObject<ProductCategory>(responseData);

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductCategory category, IFormFile imageFile)
        {
            if (ModelState.ContainsKey("ImageFile"))
            {
                ModelState.Remove("ImageFile");
            }
            if (ModelState.ContainsKey("ImageUrl"))
            {
                ModelState.Remove("ImageUrl");
            }
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    var imagePath = Path.Combine("wwwroot/images", imageFile.FileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    category.ImageUrl = $"/images/{imageFile.FileName}";
                }

                var categoryContent = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");
                var apiResponse = await _httpClient.PutAsync($"http://localhost:21905/api/productcategory/{category.CategoryId}", categoryContent);

                if (apiResponse.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                var resultMessage = await apiResponse.Content.ReadAsStringAsync();
                ModelState.AddModelError("", resultMessage);
            }

            return View(category);
        }




        [HttpGet]
        public async Task<IActionResult> Delete(int categoryId)
        {
            var response = await _httpClient.GetAsync($"http://localhost:21905/api/productcategory/{categoryId}");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var category = JsonConvert.DeserializeObject<ProductCategory>(responseData);

            var productsResponse = await _httpClient.GetAsync($"http://localhost:21905/api/productcategory/{categoryId}/products");

            if (!productsResponse.IsSuccessStatusCode)
            {
                return View("Error");
            }

            var productsData = await productsResponse.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<dynamic>(productsData);

            var products = JsonConvert.DeserializeObject<List<Product>>(Convert.ToString(result.products));

            ViewBag.ProductsToDelete = products;

            return View(category);
        }



        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int categoryId)
        {
            var apiResponse = await _httpClient.DeleteAsync($"http://localhost:21905/api/productcategory/{categoryId}");

            if (apiResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            var resultMessage = await apiResponse.Content.ReadAsStringAsync();
            ModelState.AddModelError("", resultMessage);

            return View("Error");
        }


    }
}
