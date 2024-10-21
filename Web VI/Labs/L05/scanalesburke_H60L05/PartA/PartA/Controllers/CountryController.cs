using Microsoft.AspNetCore.Mvc;
using ServiceReferenceCountryInfo;
using PartA.Models;

namespace PartA.Controllers
{
    public class CountryController : Controller
    {
        private readonly CountryInfoServiceSoapTypeClient _serviceClient;

        public CountryController()
        {
            // Choose the appropriate constructor based on your setup
            _serviceClient = new CountryInfoServiceSoapTypeClient(
                CountryInfoServiceSoapTypeClient.EndpointConfiguration.BasicHttpBinding_ICountryInfoService);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new CountryInfo());
        }
    }
}
