using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scbH60Store.DAL;
using scbH60Store.Models;
using System.Threading.Tasks;

namespace scbH60Customer.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerProductController : Controller
    {
        private readonly IProductQueryService _productQueryService;

        public CustomerProductController(IProductQueryService productQueryService)
        {
            _productQueryService = productQueryService;
        }

        // Display all products
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productQueryService.GetAllProductsAsync();
            if (products == null) return View("Error");

            // Exclude sensitive fields like BuyPrice for customers
            products.ForEach(p => p.BuyPrice = null);

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> ProductsByCategory()
        {
            var productCategories = await _productQueryService.GetProductsByCategoryAsync();
            if (productCategories == null) return View("Error");

            return View(productCategories);
        }

        // View details of a single product
        [HttpGet]
        public async Task<IActionResult> Details(int productId)
        {
            var product = await _productQueryService.GetProductDetailsAsync(productId);
            if (product == null) return View("Error");

            product.BuyPrice = null; // Hide buy price from customers
            return View(product);
        }

        // Filter and sort products
        [HttpGet]
        public async Task<IActionResult> FilterAndSort(string partialName, decimal? equalTo, decimal? lessThan, decimal? greaterThan, string sortBy = "description")
        {
            var products = await _productQueryService.FilterAndSortProductsAsync(partialName, equalTo, lessThan, greaterThan, sortBy);
            if (products == null) return View("Error");

            // Exclude sensitive fields for customer visibility
            products.ForEach(p => p.BuyPrice = null);

            ViewBag.PartialName = partialName;
            ViewBag.EqualTo = equalTo;
            ViewBag.LessThan = lessThan;
            ViewBag.GreaterThan = greaterThan;
            ViewBag.CurrentSort = sortBy;

            return View("Index", products);
        }
    }
}
