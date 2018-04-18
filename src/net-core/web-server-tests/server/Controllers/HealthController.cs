using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("health")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return NoContent();
        }
    }
}
