using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Blog.Domain.core.User.AppService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Presentation.RazorPages.Pages
{
    public class LoginModel(IUserAppService userAppService) : PageModel
    {
        [Required(ErrorMessage = "نام کاربری نمیتواند خالی باشد")]
        [Display(Name = "نام کاربری")]
        [BindProperty]
        public string Username { get; set; }
        [Required(ErrorMessage = "پسورد نمیتواند خالی باشد")]
        [Display(Name = "رمز عبور")]
        [BindProperty]
        public string Password { get; set; }



        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
               return Page(); 
            }
            var result = userAppService.Login(Username, Password);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Message);
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, result.Data!.Id.ToString()),
                new Claim(ClaimTypes.Role, result.Data.Role.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimPrincipal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties()
            {
                IsPersistent = true
            };
            HttpContext.SignInAsync(claimPrincipal, properties);
            return RedirectToPage("./Index");

        }
    }
}
