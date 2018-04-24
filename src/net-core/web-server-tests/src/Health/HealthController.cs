using Microsoft.AspNetCore.Mvc;

namespace Server.Health
{
    [Route("health")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(204)]
        public IActionResult Get()
        {
            return NoContent();
        }
    }
}
