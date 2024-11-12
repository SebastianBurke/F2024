using Microsoft.AspNetCore.Mvc;
using scbH60Services.DAL;
using scbH60Services.Models;

namespace scbH60Services.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GlobalSettingsController : ControllerBase
    {
        private readonly IGlobalSettingsService _globalSettingsService;

        public GlobalSettingsController(IGlobalSettingsService globalSettingsService)
        {
            _globalSettingsService = globalSettingsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGlobalSettings()
        {
            var settings = await _globalSettingsService.GetGlobalSettingsAsync();
            return Ok(settings);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGlobalSettings([FromBody] GlobalSettings settings)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultMessage = await _globalSettingsService.UpdateGlobalSettingsAsync(settings);
            if (resultMessage.Contains("successfully"))
            {
                return Ok(resultMessage);
            }
            return BadRequest(resultMessage);
        }
    }
}
