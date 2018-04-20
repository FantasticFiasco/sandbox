using Microsoft.AspNetCore.Mvc;

namespace Server.Health
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
