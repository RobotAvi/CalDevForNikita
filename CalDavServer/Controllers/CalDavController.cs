using Microsoft.AspNetCore.Mvc;

namespace CalDavServer.Controllers
{
    [ApiController]
    [Route("caldav")]
    public class CalDavController : ControllerBase
    {
        // TODO: Реализовать обработку PROPFIND, REPORT, PUT, DELETE, MKCALENDAR и др.
        [HttpGet("ping")]
        public IActionResult Ping() => Ok("CalDAV server is running");
    }
}