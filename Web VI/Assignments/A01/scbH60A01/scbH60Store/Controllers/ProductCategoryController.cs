using Microsoft.AspNetCore.Mvc;
using scbH60Store.Models;

namespace scbH60Store.Controllers
{
    public class ProductCategoryController : Controller
    {
        private readonly ProductCategoryService _categoryService;

        public ProductCategoryController(ProductCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return View(categories);
        }
    }
}
