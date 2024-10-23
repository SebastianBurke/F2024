using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using scbH60Store.DAL;
using scbH60Store.Models;

namespace scbH60Store.Controllers
{
    [Authorize(Roles = "Clerk,Manager")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IImageService _imageService;
        private readonly IProductValidator _productValidator;
        private readonly IProductQueryService _productQueryService;

        public ProductController(IProductService productService, IImageService imageService, IProductValidator productValidator, IProductQueryService productQueryService)
        {
            _productService = productService;
            _imageService = imageService;
            _productValidator = productValidator;
            _productQueryService = productQueryService;
        }

        // Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _productService.GetProductCategoriesAsync();
            if (categories != null)
            {
                ViewBag.CategoryList = new SelectList(categories, "CategoryId", "ProdCat");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product, IFormFile imageFile)
        {
            RemoveModelStateEntries("ProdCat", "ImageFile");

            var settings = await _productService.GetGlobalSettingsAsync();
            if (settings == null) return View("Error");

            ValidateStock(product, settings);

            product.ImageUrl = await SaveProductImage(imageFile, null);

            if (ModelState.IsValid)
            {
                var response = await _productService.CreateProductAsync(product);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                AddApiErrorToModelState(response);
            }

            await PrepareCategoryListForView();

            return View(product);
        }

        // Read
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productQueryService.GetAllProductsAsync();
            if (products == null) return View("Error");

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> ProductsByCategory()
        {
            var productCategories = await _productQueryService.GetProductsByCategoryAsync();
            if (productCategories == null) return View("Error");

            return View(productCategories);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int productId)
        {
            var product = await _productQueryService.GetProductDetailsAsync(productId);
            if (product == null) return View("Error");

            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> FilterAndSort(string partialName, decimal? equalTo, decimal? lessThan, decimal? greaterThan, string sortBy = "description")
        {
            var products = await _productQueryService.FilterAndSortProductsAsync(partialName, equalTo, lessThan, greaterThan, sortBy);
            if (products == null) return View("Error");

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
            var product = await _productService.GetProductByIdAsync(productId);
            if (product == null) return View("Error");

            await PrepareCategoryListForView();

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("ProductId, ProdCatId, Description, Manufacturer, Stock, BuyPrice, SellPrice, EmployeeNotes, ImageUrl")] Product product, IFormFile imageFile)
        {
            RemoveModelStateEntries("ProdCat", "ImageFile", "ImageUrl");

            if (imageFile == null && string.IsNullOrEmpty(product.ImageUrl))
            {
                ModelState.AddModelError("ImageUrl", "An image is required.");
            }

            product.ImageUrl = await SaveProductImage(imageFile, product.ImageUrl);

            if (ModelState.IsValid)
            {
                var response = await _productService.UpdateProductAsync(product);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                await AddApiErrorToModelState(response);
            }

            await PrepareCategoryListForView();

            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> EditStock(int productId)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            if (product == null) return View("Error");

            return View(product);
        }


        [HttpPost]
        public async Task<IActionResult> EditStock(int productId, int stockChange)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            var settings = await _productService.GetGlobalSettingsAsync();
            if (product == null || settings == null) return View("Error");

            product.Stock += stockChange;
            ValidateStock(product, settings);

            if (ModelState.IsValid)
            {
                var response = await _productService.UpdateProductStockAsync(productId, stockChange);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                await AddApiErrorToModelState(response);
            }

            // Fetch product again for re-display
            product = await _productService.GetProductByIdAsync(productId);
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> EditPrice(int productId)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            if (product == null) return RedirectToAction("NotFound", "Home");

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> EditPrice(Product product)
        {
            RemoveModelStateEntries("ProdCat", "Description");

            if (ModelState.IsValid)
            {
                var response = await _productService.UpdateProductPriceAsync(product.ProductId, product.BuyPrice, product.SellPrice);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                await AddApiErrorToModelState(response);
            }

            return View(product);
        }

        //Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int productId)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            if (product == null) return RedirectToAction("NotFound", "Home");

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int productId)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            if (product == null) return RedirectToAction("NotFound", "Home");

            DeleteProductImage(product.ImageUrl);

            var response = await _productService.DeleteProductAsync(productId);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View("Error");
        }

        // Helper Methods
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

        private void ValidateStock(Product product, GlobalSettings settings)
        {
            if (!_productValidator.ValidateStock(product, settings, out var errorMessage))
            {
                ModelState.AddModelError("Stock", errorMessage);
            }
        }

        private async Task<string> SaveProductImage(IFormFile imageFile, string existingImageUrl)
        {
            var defaultImage = "/images/default-image.png";
            return await _imageService.SaveImageAsync(imageFile, defaultImage, existingImageUrl);
        }

        private void DeleteProductImage(string imageUrl)
        {
            if (imageUrl != "/images/default-image.png")
            {
                var imagePath = Path.Combine("wwwroot", imageUrl.TrimStart('/'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
        }

        private async Task AddApiErrorToModelState(HttpResponseMessage response)
        {
            var resultMessage = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", resultMessage);
        }

        private async Task PrepareCategoryListForView()
        {
            var categories = await _productService.GetProductCategoriesAsync();
            if (categories != null)
            {
                ViewBag.CategoryList = new SelectList(categories, "CategoryId", "ProdCat");
            }
        }
    }
}
