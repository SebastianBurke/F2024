using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using scbH60Store.Models;
using System.Threading.Tasks;

namespace scbH60Store.Controllers
{
    public class ProductCategoryController : Controller
    {
        private readonly IProductCategoryService _categoryService;

        public ProductCategoryController(IProductCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCategory category, IFormFile imageFile)
        {
            if (ModelState.ContainsKey("ImageFile"))
            {
                ModelState.Remove("ImageFile");
            }
            if (ModelState.IsValid)
            {
                // Handle image file
                if (imageFile != null && imageFile.Length > 0)
                {
                    var imagePath = Path.Combine("wwwroot/images", imageFile.FileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    category.ImageUrl = $"/images/{imageFile.FileName}";
                }
                else
                {
                    // Assign default image if no image is uploaded
                    category.ImageUrl = "/images/default-image.png";
                }

                await _categoryService.AddCategory(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }


        // Read
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategories();
            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> CategoryProducts(int id)
        {
            var products = await _categoryService.GetCategoryProducts(id);
            var category = await _categoryService.GetCategoryById(id);
            ViewBag.CategoryName = category != null ? category.ProdCat : "Unknown Category";
            ViewBag.CategoryId = category.CategoryId;
            return View(products);
        }

        // Update
        [HttpGet]
        public async Task<IActionResult> Edit(int categoryId)
        {
            var category = await _categoryService.GetCategoryById(categoryId);
            if (category == null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Edit([Bind("CategoryId, ProdCat, ImageUrl")] ProductCategory category, IFormFile imageFile)
        {
            if (ModelState.ContainsKey("ImageFile"))
            {
                ModelState.Remove("ImageFile");
            }

            // If no image is uploaded and no existing image URL is present, add a model error
            if (imageFile == null && string.IsNullOrEmpty(category.ImageUrl))
            {
                ModelState.AddModelError("ImageUrl", "An image is required.");
            }

            if (ModelState.IsValid)
            {

                if (imageFile != null && imageFile.Length > 0)
                {
                    // Delete old image if it is not the default image
                    if (category.ImageUrl != "/images/default-image.png")
                    {
                        var oldImagePath = Path.Combine("wwwroot", category.ImageUrl.TrimStart('/'));
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
                    category.ImageUrl = $"/images/{imageFile.FileName}";
                }

                try
                {
                    await _categoryService.UpdateCategory(category);
                    return RedirectToAction("Index");
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(category);
        }



        // Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int categoryId)
        {
            var category = await _categoryService.GetCategoryById(categoryId);
            if (category == null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            var products = await _categoryService.GetCategoryProducts(categoryId);

            ViewBag.ProductsToDelete = products;

            return View(category);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int categoryId)
        {
            var category = await _categoryService.GetCategoryById(categoryId);

            if (category == null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            // Delete image file if it's not the default image
            if (category.ImageUrl != "/images/default-image.png")
            {
                var imagePath = Path.Combine("wwwroot", category.ImageUrl.TrimStart('/'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            await _categoryService.DeleteCategory(categoryId);
            return RedirectToAction("Index");
        }

    }
}
