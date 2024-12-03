using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scbH60Store.DAL;
using scbH60Store.Models;
using System.Diagnostics;

namespace scbH60Store.Controllers
{
    [Authorize(Roles = "Clerk,Manager")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductAPIService _productService;
        private readonly IProductCategoryAPIService _categoryService;

        public HomeController(ILogger<HomeController> logger, IProductAPIService productService, IProductCategoryAPIService categoryService)
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
