using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scbH60Store.DAL;
using scbH60Store.Models;

namespace scbH60Store.Controllers
{
    [Authorize(Roles = "Clerk,Manager")]
    public class ProductCategoryController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IProductCategoryService _productCategoryService;
        private readonly IImageService _imageService;

        public ProductCategoryController(IProductCategoryService productCategoryService, IImageService imageService)
        {
            _productCategoryService = productCategoryService;
            _imageService = imageService;
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
            RemoveModelStateEntries("ImageFile", "ImageUrl");

            category.ImageUrl = await SaveProductCategoryImage(imageFile);

            if (ModelState.IsValid)
            {
                var response = await _productCategoryService.CreateCategoryAsync(category);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                await AddApiErrorToModelState(response);
            }

            return View(category);
        }

        // Read

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _productCategoryService.GetAllCategoriesAsync();
            if (categories == null) return View("Error");

            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> CategoryProducts(int id)
        {
            var result = await _productCategoryService.GetProductsByCategoryAsync(id);
            if (result == null || result.Products == null) return View("Error");

            ViewBag.CategoryName = result.CategoryName ?? "Unknown Category";
            ViewBag.CategoryId = id;

            return View(result.Products);
        }

        // Update

        [HttpGet]
        public async Task<IActionResult> Edit(int categoryId)
        {
            var category = await _productCategoryService.GetCategoryByIdAsync(categoryId);
            if (category == null) return View("Error");

            return View(category);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(ProductCategory category, IFormFile imageFile)
        {
            RemoveModelStateEntries("ImageFile", "ImageUrl");

            category.ImageUrl = await SaveProductCategoryImage(imageFile, category.ImageUrl);

            if (ModelState.IsValid)
            {
                var response = await _productCategoryService.UpdateCategoryAsync(category);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                await AddApiErrorToModelState(response);
            }

            return View(category);
        }

        //Delete

        [HttpGet]
        public async Task<IActionResult> Delete(int categoryId)
        {
            var category = await _productCategoryService.GetCategoryByIdAsync(categoryId);
            if (category == null) return View("Error");

            var products = await _productCategoryService.GetProductsByCategoryAsync(categoryId);
            ViewBag.ProductsToDelete = products?.Products;

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int categoryId)
        {
            var response = await _productCategoryService.DeleteCategoryAsync(categoryId);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            await AddApiErrorToModelState(response);
            return View("Error");
        }

        // Helper methods

        private void RemoveModelStateEntries(params string[] keys)
        {
            foreach (var key in keys)
            {
                if (ModelState.ContainsKey(key))
                {
                    ModelState.Remove(key);
                }
            }
        }

        private async Task<string> SaveProductCategoryImage(IFormFile imageFile, string existingImageUrl = null)
        {
            var defaultImage = "/images/default-image.png";
            return await _imageService.SaveImageAsync(imageFile, defaultImage, existingImageUrl);
        }

        private async Task AddApiErrorToModelState(HttpResponseMessage response)
        {
            var resultMessage = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", resultMessage);
        }

        // Matching model for the deserialization
        public class CategoryProductsResponse
        {
            public string CategoryName { get; set; }
            public List<Product> Products { get; set; }
        }

    }
}
