using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scbH60Store.DAL;
using scbH60Store.Models;
using scbH60Services.Models;

namespace scbH60Store.Controllers
{
    [Authorize(Roles = "Manager")]
    public class GlobalSettingsController : Controller
    {
        private readonly IGlobalSettingsAPIService _globalSettingsApiService;

        public GlobalSettingsController(IGlobalSettingsAPIService globalSettingsApiService)
        {
            _globalSettingsApiService = globalSettingsApiService;
        }

        public async Task<IActionResult> Index()
        {
            var settings = await _globalSettingsApiService.GetGlobalSettingsAsync();
            if (settings == null) return View("Error");

            return View(settings);
        }

        [HttpPost]
        public async Task<IActionResult> Update(GlobalSettings settings)
        {
            if (ModelState.IsValid)
            {
                var resultMessage = await _globalSettingsApiService.UpdateGlobalSettingsAsync(settings);
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