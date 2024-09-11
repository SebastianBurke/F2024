using Microsoft.AspNetCore.Mvc;
using scbH60Store.DAL;
using scbH60Store.Models;

namespace scbH60Store.Controllers
{
    public class GlobalSettingsController : Controller
    {
        private readonly IGlobalSettingsService _globalSettingsService;

        public GlobalSettingsController(IGlobalSettingsService globalSettingsService)
        {
            _globalSettingsService = globalSettingsService;
        }

        public async Task<IActionResult> Index()
        {
            var settings = await _globalSettingsService.GetGlobalSettingsAsync();
            return View(settings);
        }

        [HttpPost]
        public async Task<IActionResult> Update(GlobalSettings settings)
        {
            if (ModelState.IsValid)
            {
                var resultMessage = await _globalSettingsService.UpdateGlobalSettingsAsync(settings);
                TempData["Message"] = resultMessage;
                if (resultMessage.Contains("successfully"))
                {
                    return RedirectToAction("Index");
                }
            }
            return View("Index", settings);
        }
    }

}
