using api.DTOs;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _service;

        public PersonController(IPersonService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDTO>>> GetAll(string? searchTerm)
        {
            var persons = await _service.GetAllAsync(searchTerm);
            return Ok(persons);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDTO>> GetById(int id)
        {
            var person = await _service.GetByIdAsync(id);
            return person == null ? NotFound() : Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PersonDTO personDTO)
        {
            await _service.AddAsync(personDTO);
            return CreatedAtAction(nameof(GetById), new { id = personDTO.Id }, personDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PersonDTO personDTO)
        {
            await _service.UpdateAsync(id, personDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("withpets")]
        public async Task<ActionResult<IEnumerable<PersonDTO>>> GetPersonsWithPets()
        {
            var persons = await _service.GetPersonsWithPetsAsync();
            return Ok(persons);
        }

    }

}
