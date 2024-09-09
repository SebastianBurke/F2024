using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.ContainsKey("ProdCat"))
            {
                ModelState.Remove("ProdCat");
            }

            if (ModelState.IsValid)
            {
                await _productService.AddProduct(product);
                return RedirectToAction("Index");
            }

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
        public async Task<IActionResult> Details(int productId)
        {
            var product = await  _productService.GetProductById(productId);
            if (product == null) return NotFound();

            return View(product);
        }

        // Update
        [HttpGet]
        public async Task<IActionResult> Edit(int productId)
        {
            var product = await _productService.GetProductById(productId);
            if (product == null) return NotFound();

            var categories = await _categoryService.GetAllCategories();
            ViewBag.CategoryList = new SelectList(categories, "CategoryId", "ProdCat");

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.ContainsKey("ProdCat"))
            {
                ModelState.Remove("ProdCat");
            }

            if (ModelState.IsValid)
            {
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
            if (product == null) return NotFound();

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
            if (product == null) return NotFound();

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
            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int productId)
        {
            await _productService.DeleteProduct(productId);
            return RedirectToAction("Index");
        }

    }
}
