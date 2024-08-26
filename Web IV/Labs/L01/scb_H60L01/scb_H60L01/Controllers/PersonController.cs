using Microsoft.AspNetCore.Mvc;
using scb_H60L01.Models;

public class PersonController : Controller
{
    public IActionResult Index()
    {
        List<Person> personList = Person.GetPersons();
        return View(personList);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(Person person)
    {
        if (ModelState.IsValid)
        {
            person.AddPerson();
            TempData["Message"] = "Person Added Successfully";
            return RedirectToAction("Index");
        }

        return View(person);
    }
}
