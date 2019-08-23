using DummyTodoApp.WebApi.Settings;
using Microsoft.AspNetCore.Mvc;

namespace DummyTodoApp.WebApi.Controllers.GetSettings
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        readonly DummySettings dummySettings;

        public SettingsController(DummySettings settings) => dummySettings = settings;
        
        [HttpGet]
        public IActionResult Get() 
        {
            return Ok(dummySettings);
        }
    }
}