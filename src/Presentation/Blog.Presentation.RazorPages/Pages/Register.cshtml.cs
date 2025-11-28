using Blog.Domain.core.User.AppService;
using Blog.Domain.core.User.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Blog.Presentation.RazorPages.Pages
{
    public class RegisterModel(IUserAppService userAppService) : PageModel
    {
        [BindProperty]
        public RegisterViewModel Input { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var dto = new CreateUserDto
            {
                FirstName = Input.FirstName,
                LastName = Input.LastName,
                UserName = Input.UserName,
                Password = Input.Password,
                ImgUrl = "default.jpg"
            };

            var result = await userAppService.CreateAsync(dto);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Message!);
                return Page();
            }

            return RedirectToPage("/Login");
        }

        public class RegisterViewModel
        {
            [Display(Name = "نام")]
            [Required(ErrorMessage = "نام الزامی است")]
            public string FirstName { get; set; }

            [Display(Name = "نام خانوادگی")]
            [Required(ErrorMessage = "نام خانوادگی الزامی است")]
            public string LastName { get; set; }

            [Display(Name = "نام کاربری")]
            [Required(ErrorMessage = "نام کاربری الزامی است")]
            public string UserName { get; set; }

            [Display(Name = "رمز عبور")]
            [Required(ErrorMessage = "رمز عبور الزامی است")]
            [MinLength(6, ErrorMessage = "حداقل ۶ کاراکتر وارد کنید")]
            public string Password { get; set; }

        }
    }

}
