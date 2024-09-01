using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync();
            return View(products);
        }

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


    }
}
