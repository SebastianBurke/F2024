using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using scbH60Store.Models;

namespace scbH60Store.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        private readonly ProductCategoryService _categoryService;

        public ProductController(ProductService productService, ProductCategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }



        // Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.CategoryList = new SelectList(categories, "CategoryId", "ProdCat");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.ContainsKey("ProdCat"))
            {
                ModelState.Remove("ProdCat");
            }

            if (ModelState.IsValid)
            {
                await _productService.AddProductAsync(product);
                return RedirectToAction("Index");
            }

            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.CategoryList = new SelectList(categories, "CategoryId", "ProdCat");

            return View(product);
        }

        // Read
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int productId)
        {
            var product = await  _productService.GetProductByIdAsync(productId);
            if (product == null) return NotFound();

            return View(product);
        }

        // Update
        [HttpGet]
        public async Task<IActionResult> UpdateStock(int productId)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStock(int productId, int stockChange)
        {
            try
            {
                await _productService.UpdateStockAsync(productId, stockChange);
                return RedirectToAction("Index");
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdatePrice(int productId)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePrice(Product product)
        {
            try
            {
                await _productService.UpdatePriceAsync(product.ProductId, product.BuyPrice ?? 0, product.SellPrice ?? 0);
                return RedirectToAction("Index");
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(product);
            }
        }

        // Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int productId)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int productId)
        {
            await _productService.DeleteProductAsync(productId);
            return RedirectToAction("Index");
        }

    }
}
