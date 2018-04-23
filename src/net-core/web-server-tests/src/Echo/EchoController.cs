using Microsoft.AspNetCore.Mvc;

namespace Server.Echo
{
    [Route("echo")]
    public class EchoController : ControllerBase
    {
        [HttpGet]
        [Route("{text}")]
        public IActionResult Get(string text)
        {
            return Ok(text);
        }
    }
}
