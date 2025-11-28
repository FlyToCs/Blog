using Blog.Domain.core.User.AppService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Blog.Domain.core.User.Enums;
using Blog.Presentation.RazorPages.Services;


namespace Blog.Presentation.RazorPages.Pages
{
    public class LoginModel(IUserAppService userAppService,CookieManagementService cookieService) : PageModel
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

        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
               return Page(); 
            }
            var result = await userAppService.LoginAsync(Username, Password, cancellationToken);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Message);
            }
            else
            {
                cookieService.SignIn(
                    result.Data.Id,
                    result.Data.UserName,
                    result.Data.Role.ToString(),
                    false);

                if (result.Data.Role != RoleEnum.User)
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
            }

            return RedirectToPage("/index");

        }
    }
}
