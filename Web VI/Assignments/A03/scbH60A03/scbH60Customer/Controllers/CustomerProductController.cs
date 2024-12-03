using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scbH60Store.DAL;
using scbH60Services.Interfaces;
using scbH60Store.Models;
using System.Threading.Tasks;
using scbH60Services.Services;
using Microsoft.AspNetCore.Identity;
using scbH60Services.Models;

namespace scbH60Customer.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerProductController : Controller
    {
        private readonly IProductQueryService _productQueryService;
        private readonly ICartItemService _cartItemService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly UserManager<User> _userManager;

        public CustomerProductController(IProductQueryService productQueryService, ICartItemService cartItemService, IShoppingCartService shoppingCartService, UserManager<User> userManager)
        {
            _productQueryService = productQueryService;
            _cartItemService = cartItemService;
            _shoppingCartService = shoppingCartService;
            _userManager = userManager;
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

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            try
            {
                // Get the currently logged-in user
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return Unauthorized(); // Handle unauthenticated users
                }

                // Retrieve CustomerId from ApplicationUser
                string customerId = user.CustomerId;

                // Fetch or create the cart
                var cart = await _shoppingCartService.GetCartByCustomerId(customerId);
                if (cart == null)
                {
                    cart = await _shoppingCartService.CreateCart(customerId);
                }

                await _cartItemService.AddItemToCart(cart.ShoppingCartId, productId, quantity);

                TempData["SuccessMessage"] = "Product added to cart successfully.";
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction("Details", new { productId });
        }

        [HttpGet]
        public async Task<IActionResult> ViewCart()
        {
            // Get the currently logged-in user
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized(); // Handle unauthenticated users
            }

            // Retrieve the shopping cart for the current user
            string customerId = user.CustomerId;
            var cart = await _shoppingCartService.GetCartByCustomerId(customerId);

            if (cart == null || !cart.CartItems.Any())
            {
                TempData["InfoMessage"] = "Your cart is empty.";
                return RedirectToAction("Index");
            }

            // Pass the cart to the view
            return View(cart);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int productId)
        {
            // Fetch product details using the service
            var product = await _productQueryService.GetProductDetailsAsync(productId);

            if (product == null)
            {
                TempData["ErrorMessage"] = "Product not found.";
                return RedirectToAction("Index");
            }

            // Hide sensitive fields for customers
            product.BuyPrice = null;

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
