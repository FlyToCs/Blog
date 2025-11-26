using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;


public class CookieManagementService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string AuthenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme;

    public CookieManagementService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void SignIn(int userId, string username, string userRole, bool isPersistent)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, userRole),
        };

        var claimsIdentity = new ClaimsIdentity(claims, AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            IsPersistent = isPersistent,
            ExpiresUtc = isPersistent ? DateTimeOffset.UtcNow.AddDays(30) : (DateTimeOffset?)null
        };


        _httpContextAccessor.HttpContext.SignInAsync(
            AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties).GetAwaiter().GetResult();
    }


    public void SignOut()
    {
        _httpContextAccessor.HttpContext.SignOutAsync(AuthenticationScheme).GetAwaiter().GetResult();
    }

    public ClaimsPrincipal? GetCurrentUser()
    {
        return _httpContextAccessor.HttpContext?.User;
    }
}