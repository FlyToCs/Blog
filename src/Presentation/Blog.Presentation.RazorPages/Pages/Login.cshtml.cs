using Blog.Domain.core.User.AppService;
using Blog.Domain.core.User.Entities;
using Blog.Infa.Db.SqlServer.EfCore.Migrations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Blog.Domain.core.User.Enums;
using CodeYad_Blog.Web.Pages.Category;

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
