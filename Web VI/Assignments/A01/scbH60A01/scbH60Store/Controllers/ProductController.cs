using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using scbH60Store.Models;

namespace scbH60Store.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductCategoryService _categoryService;

        public ProductController(IProductService productService, IProductCategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
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

                // Save the product to the database
                await _productService.AddProduct(product);
                return RedirectToAction("Index");
            }

            // Re-populate category list for dropdown in case of a validation error
            var categories = await _categoryService.GetAllCategories();
            ViewBag.CategoryList = new SelectList(categories, "CategoryId", "ProdCat");
            return View(product);
        }


        // Read
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProducts();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> ProductsByCategory()
        {
            var products = await _productService.GetAllProductsByCategory();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int productId)
        {
            var product = await  _productService.GetProductById(productId);
            if (product == null) return RedirectToAction("NotFound", "Home");

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

            var products = await _productService.GetProductsFilteredAndSorted(partialName, equalTo, lessThan, greaterThan, sortBy);

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
            if (product == null) return RedirectToAction("NotFound", "Home");

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

            if (imageFile == null && string.IsNullOrEmpty(product.ImageUrl))
            {
                ModelState.AddModelError("ImageUrl", "An image is required.");
            }

            if (ModelState.IsValid)
            {

                if (imageFile != null && imageFile.Length > 0)
                {
                    // Delete old image if it is not the default image
                    if (product.ImageUrl != "/images/default-image.png")
                    {
                        var oldImagePath = Path.Combine("wwwroot", product.ImageUrl.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    var imagePath = Path.Combine("wwwroot/images", imageFile.FileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    product.ImageUrl = $"/images/{imageFile.FileName}";
                }

                try
                {
                    await _productService.Edit(product);
                    return RedirectToAction("Index");
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
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
        public async Task<IActionResult> EditStock(Product product)
        {
            if (ModelState.ContainsKey("ProdCat"))
            {
                ModelState.Remove("ProdCat");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _productService.EditStock(product.ProductId, product.Stock);
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
