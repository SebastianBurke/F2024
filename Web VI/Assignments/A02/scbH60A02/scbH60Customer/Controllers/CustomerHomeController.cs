using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scbH60Store.Models;
using System.Diagnostics;

namespace scbH60Service.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerHomeController : Controller
    {
        private readonly ILogger<CustomerHomeController> _logger;
        private readonly IProductService _productService;
        private readonly IProductCategoryService _categoryService;

        public CustomerHomeController(ILogger<CustomerHomeController> logger, IProductService productService, IProductCategoryService categoryService)
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            // Fetch categories and include related products
            var categories = await _productService.GetAllProductsByCategoryAsync();


            // Select a random category
            var randomCategory = categories.OrderBy(c => Guid.NewGuid()).FirstOrDefault();

            // Pass the data to the view
            ViewBag.Categories = categories;
            ViewBag.RandomCategory = randomCategory;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                if (statusCode == 404)
                {
                    return View("NotFound");
                }
            }

            // Default error view
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
