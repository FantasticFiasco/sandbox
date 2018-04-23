using Microsoft.AspNetCore.Mvc;

namespace Server.Echo
{
    [Route("echo")]
    public class EchoController : ControllerBase
    {
        [HttpGet]
        [Route("{text}")]
        [ProducesResponseType(200)]
        public IActionResult Get(string text)
        {
            return Ok(text);
        }
    }
}
