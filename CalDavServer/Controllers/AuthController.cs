using Microsoft.AspNetCore.Mvc;

namespace CalDavServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] object credentials)
        {
            // TODO: Реализовать аутентификацию (Basic, OAuth2)
            return Ok(new { token = "dummy-token" });
        }
    }
}