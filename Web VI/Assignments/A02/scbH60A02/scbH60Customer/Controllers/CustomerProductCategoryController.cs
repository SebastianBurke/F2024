using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scbH60Store.DAL;
using scbH60Store.Models;
using System.Threading.Tasks;

namespace scbH60Customer.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerProductCategoryController : Controller
    {
        private readonly IProductCategoryService _productCategoryService;

        public CustomerProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

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

        [HttpGet]
        public async Task<IActionResult> ProductsByCategory(int categoryId)
        {
            var result = await _productCategoryService.GetProductsByCategoryAsync(categoryId);
            if (result == null || result.Products == null) return View("Error");

            ViewBag.CategoryName = result.CategoryName ?? "Unknown Category";
            ViewBag.CategoryId = categoryId;

            result.Products.ForEach(p => p.BuyPrice = null);

            return View(result.Products);
        }
    }
}
