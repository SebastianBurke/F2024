using Microsoft.AspNetCore.Mvc;
using scbH60Services.DAL;
using scbH60Services.Models;

namespace scbH60Services.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductCategoryService _productCategoryService;

        public ProductsController(IProductService productService, IProductCategoryService productCategoryService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
        }

        [HttpPost] 
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            var resultMessage = await _productService.AddProduct(product);

            if (resultMessage.Contains("successfully"))
            {
                return Ok(resultMessage);
            }

            return BadRequest(resultMessage);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("bycategory")]
        public async Task<IActionResult> ProductsByCategory()
        {
            var products = await _productService.GetAllProductsByCategory();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet("filterandsort")]
        public async Task<IActionResult> FilterAndSort(
        [FromQuery] string? partialName,
        [FromQuery] decimal? equalTo,
        [FromQuery] decimal? lessThan,
        [FromQuery] decimal? greaterThan,
        [FromQuery] string sortBy = "description")
        {
            var products = await _productService.GetProductsFilteredAndSorted(partialName, equalTo, lessThan, greaterThan, sortBy);

            return Ok(products);
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _productCategoryService.GetAllCategories();
            return Ok(categories);
        }


    }
}
