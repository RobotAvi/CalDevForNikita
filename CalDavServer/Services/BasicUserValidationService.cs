using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Basic;
using Microsoft.AspNetCore.Authentication.Basic.Events;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using CalDavServer.Services;

public class BasicUserValidationService : IBasicUserValidationService
{
    private readonly AuthService _authService;
    public BasicUserValidationService(AuthService authService)
    {
        _authService = authService;
    }
    public Task<BasicUserValidationResult> ValidateAsync(string username, string password)
    {
        var token = _authService.Authenticate(username, password);
        if (token != null)
        {
            var claims = new[] { new Claim(ClaimTypes.Name, username) };
            var identity = new ClaimsIdentity(claims, BasicAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            return Task.FromResult(BasicUserValidationResult.Success(principal));
        }
        return Task.FromResult(BasicUserValidationResult.Fail("Invalid username or password"));
    }
}