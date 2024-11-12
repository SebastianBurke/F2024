using Microsoft.AspNetCore.Mvc;
using scbH60Services.DAL;
using scbH60Services.Models;
using System.Threading.Tasks;

namespace scbH60Services.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomers();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(string id)
        {
            var customer = await _customerService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound("Customer not found.");
            }
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] User customer) // Replace User with your custom model if different
        {
            var resultMessage = await _customerService.AddCustomer(customer);
            return resultMessage.Contains("successfully") ? Ok(resultMessage) : BadRequest(resultMessage);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(string id, [FromBody] User customer)
        {
            if (id != customer.Id)
            {
                return BadRequest("Customer ID mismatch.");
            }

            var resultMessage = await _customerService.UpdateCustomer(customer);
            return resultMessage.Contains("successfully") ? Ok(resultMessage) : BadRequest(resultMessage);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            var resultMessage = await _customerService.DeleteCustomer(id);
            return resultMessage.Contains("successfully") ? Ok(resultMessage) : BadRequest(resultMessage);
        }
    }
}
