using Microsoft.AspNetCore.Mvc;

namespace Server.Echo
{
    [Route("echo")]
    public class EchoController : ControllerBase
    {
        [HttpGet]
        [Route("{text}")]
        public string Get(string text)
        {
            return text;
        }
    }
}
