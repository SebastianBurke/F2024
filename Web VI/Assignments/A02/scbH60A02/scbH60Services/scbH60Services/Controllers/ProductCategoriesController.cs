using Microsoft.AspNetCore.Mvc;
using scbH60Services.Models;
using scbH60Services.DAL;
using System.Threading.Tasks;

namespace scbH60Services.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] ProductCategory category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _productCategoryService.AddCategory(category);
            return Ok("Category created successfully");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _productCategoryService.GetAllCategories();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductCategoryById(int id)
        {
            var category = await _productCategoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetCategoryProducts(int id)
        {
            var category = await _productCategoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound("Category not found.");
            }

            var products = await _productCategoryService.GetCategoryProducts(id);

            var result = new
            {
                CategoryName = category.ProdCat,
                Products = products
            };

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] ProductCategory category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _productCategoryService.UpdateCategory(category);
                return Ok("Category updated successfully");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _productCategoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound("Category not found");
            }

            await _productCategoryService.DeleteCategory(id);
            return Ok("Category deleted successfully");
        }
    }
}
