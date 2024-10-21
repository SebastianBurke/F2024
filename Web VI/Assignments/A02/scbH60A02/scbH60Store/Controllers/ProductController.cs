using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using scbH60Store.DAL;
using scbH60Store.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace scbH60Store.Controllers
{
    [Authorize(Roles = "Clerk,Manager")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductCategoryService _categoryService;
        private readonly IGlobalSettingsService _globalSettingsService;
        private readonly HttpClient _httpClient;

        public ProductController(IProductService productService, IProductCategoryService categoryService, IGlobalSettingsService globalSettingsService, HttpClient httpClient)
        {
            _productService = productService;
            _categoryService = categoryService;
            _globalSettingsService = globalSettingsService;
            _httpClient = httpClient;
        }

        // Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllCategories();
            ViewBag.CategoryList = new SelectList(categories, "CategoryId", "ProdCat");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product, IFormFile imageFile)
        {

            if (ModelState.ContainsKey("ProdCat"))
            {
                ModelState.Remove("ProdCat");
            }
            if (ModelState.ContainsKey("ImageFile"))
            {
                ModelState.Remove("ImageFile");
            }
            // Retrieve global settings
            var settings = await _globalSettingsService.GetGlobalSettingsAsync();

            // Check if Stock is within the global settings limits
            if (product.Stock < settings.MinStockLimit || product.Stock > settings.MaxStockLimit)
            {
                ModelState.AddModelError("Stock", $"Stock must be between {settings.MinStockLimit} and {settings.MaxStockLimit}.");
            }

            if (ModelState.IsValid)
            {
                // Handle the image file
                if (imageFile != null && imageFile.Length > 0)
                {
                    var imagePath = Path.Combine("wwwroot/images", imageFile.FileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    product.ImageUrl = $"/images/{imageFile.FileName}";
                }
                else
                {
                    // Set a default image if none is provided
                    product.ImageUrl = "/images/default-image.png";
                }

                // Add the product
                var resultMessage = await _productService.AddProduct(product);
                if (resultMessage.Contains("successfully"))
                {
                    return RedirectToAction("Index");
                }

                // Add error message to ModelState
                ModelState.AddModelError("", resultMessage);
            }

            // Re-populate category list for dropdown in case of a validation error
            var categories = await _categoryService.GetAllCategories();
            ViewBag.CategoryList = new SelectList(categories, "CategoryId", "ProdCat");
            return View(product);
        }


        // Read
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("http://localhost:21905/api/products");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<List<Product>>(responseData);

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> ProductsByCategory()
        {
            var response = await _httpClient.GetAsync("http://localhost:21905/api/products/bycategory");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var productCategories = JsonConvert.DeserializeObject<List<ProductCategory>>(responseData);

            return View(productCategories);
        }


        [HttpGet]
        public async Task<IActionResult> Details(int productId)
        {
            var response = await _httpClient.GetAsync($"http://localhost:21905/api/products/{productId}");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error"); 
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<Product>(responseData);

            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> FilterAndSort(
            string partialName,
            decimal? equalTo,
            decimal? lessThan,
            decimal? greaterThan,
            string sortBy = "description")
        {
            Console.WriteLine($"partialName: {partialName}, equalTo: {equalTo}, lessThan: {lessThan}, greaterThan: {greaterThan}, sortBy: {sortBy}");

            var queryParams = new List<string>();

            if (!string.IsNullOrEmpty(partialName))
                queryParams.Add($"partialName={partialName}");

            if (equalTo.HasValue)
                queryParams.Add($"equalTo={equalTo}");

            if (lessThan.HasValue)
                queryParams.Add($"lessThan={lessThan}");

            if (greaterThan.HasValue)
                queryParams.Add($"greaterThan={greaterThan}");

            queryParams.Add($"sortBy={sortBy}");

            var queryString = string.Join("&", queryParams);

            var response = await _httpClient.GetAsync($"http://localhost:21905/api/products/filterandsort?{queryString}");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<List<Product>>(responseData);

            ViewBag.PartialName = partialName;
            ViewBag.EqualTo = equalTo;
            ViewBag.LessThan = lessThan;
            ViewBag.GreaterThan = greaterThan;
            ViewBag.CurrentSort = sortBy;

            return View("Index", products);
        }


        // Update
        [HttpGet]
        public async Task<IActionResult> Edit(int productId)
        {
            var product = await _productService.GetProductById(productId);
            if (product == null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            var categories = await _categoryService.GetAllCategories();
            ViewBag.CategoryList = new SelectList(categories, "CategoryId", "ProdCat");

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("ProductId, ProdCatId, Description, Manufacturer, Stock, BuyPrice, SellPrice, EmployeeNotes, ImageUrl")] Product product, IFormFile imageFile)
        {
            if (ModelState.ContainsKey("ProdCat"))
            {
                ModelState.Remove("ProdCat");
            }
            if (ModelState.ContainsKey("ImageFile"))
            {
                ModelState.Remove("ImageFile");
            }
            if (ModelState.ContainsKey("ImageUrl"))
            {
                ModelState.Remove("ImageUrl");
            }

            if (imageFile == null && string.IsNullOrEmpty(product.ImageUrl))
            {
                ModelState.AddModelError("ImageUrl", "An image is required.");
            }

            if (ModelState.IsValid)
            {
                var resultMessage = await _productService.Edit(product, imageFile);
                if (resultMessage.Contains("successfully"))
                {
                    return RedirectToAction("Index");
                }

                // Add error message to ModelState
                ModelState.AddModelError("", resultMessage);
            }

            var categories = await _categoryService.GetAllCategories();
            ViewBag.CategoryList = new SelectList(categories, "CategoryId", "ProdCat");
            return View(product);
        }



        [HttpGet]
        public async Task<IActionResult> EditStock(int productId)
        {
            var product = await _productService.GetProductById(productId);
            if (product == null) return RedirectToAction("NotFound", "Home");

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> EditStock(int ProductId, int StockChange)
        {
            // Retrieve global settings
            var settings = await _globalSettingsService.GetGlobalSettingsAsync();

            // Find the product
            var product = await _productService.GetProductById(ProductId);
            if (product == null)
            {
                ModelState.AddModelError("", "Product not found.");
                return View(product);
            }

            // Calculate the new stock value
            var newStock = product.Stock + StockChange;

            // Validate new stock value against global settings
            if (newStock < settings.MinStockLimit || newStock > settings.MaxStockLimit)
            {
                ModelState.AddModelError("", $"Stock after adjustment must be between {settings.MinStockLimit} and {settings.MaxStockLimit}.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Update stock
                    await _productService.EditStock(ProductId, StockChange);
                    return RedirectToAction("Index");
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(product);
        }


        [HttpGet]
        public async Task<IActionResult> EditPrice(int productId)
        {
            var product = await _productService.GetProductById(productId);
            if (product == null) return RedirectToAction("NotFound", "Home");

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> EditPrice(Product product)
        {
            if (ModelState.ContainsKey("ProdCat"))
            {
                ModelState.Remove("ProdCat");
            }
            if (ModelState.ContainsKey("Description"))
            {
                ModelState.Remove("Description");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _productService.EditPrice(product.ProductId, product.BuyPrice ?? 0, product.SellPrice ?? 0);
                    return RedirectToAction("Index");
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(product);
        }



        // Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int productId)
        {
            var product = await _productService.GetProductById(productId);
            if (product == null) return RedirectToAction("NotFound", "Home");

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int productId)
        {
            var product = await _productService.GetProductById(productId);

            if (product == null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            // Delete image file if it's not the default image
            if (product.ImageUrl != "/images/default-image.png")
            {
                var imagePath = Path.Combine("wwwroot", product.ImageUrl.TrimStart('/'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            await _productService.DeleteProduct(productId);
            return RedirectToAction("Index");
        }


    }
}
