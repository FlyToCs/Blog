using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Blog.Presentation.RazorPages.Services;

public class CookieManagementService(IHttpContextAccessor httpContextAccessor)
{
    private const string AuthenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme;

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


        if (httpContextAccessor.HttpContext != null)
            httpContextAccessor.HttpContext.SignInAsync(
                AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties).GetAwaiter().GetResult();
    }


    public void SignOut()
    {
        if (httpContextAccessor.HttpContext != null)
            httpContextAccessor.HttpContext.SignOutAsync(AuthenticationScheme).GetAwaiter().GetResult();
    }

    public ClaimsPrincipal? GetCurrentUser()
    {
        return httpContextAccessor.HttpContext?.User;
    }
}