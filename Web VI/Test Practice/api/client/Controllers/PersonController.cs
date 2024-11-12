using client.Services;
using client.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace client.Controllers
{
    public class PersonController : Controller
    {
        private readonly ApiService _apiService;

        public PersonController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index(string? searchTerm)
        {
            var persons = await _apiService.GetAllPersonsAsync(searchTerm);
            ViewData["searchTerm"] = searchTerm;
            return View(persons);
        }

        public async Task<IActionResult> Details(int id)
        {
            var person = await _apiService.GetPersonByIdAsync(id);
            return View(person);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PersonDTO person)
        {
            if (ModelState.IsValid)
            {
                await _apiService.AddPersonAsync(person);
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var person = await _apiService.GetPersonByIdAsync(id);
            return View(person);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, PersonDTO person)
        {
            if (ModelState.IsValid)
            {
                await _apiService.UpdatePersonAsync(id, person);
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var person = await _apiService.GetPersonByIdAsync(id);
            return View(person);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _apiService.DeletePersonAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> PersonsWithPets()
        {
            var personsWithPets = await _apiService.GetPersonsWithPetsAsync();
            return View(personsWithPets);
        }

    }
}
