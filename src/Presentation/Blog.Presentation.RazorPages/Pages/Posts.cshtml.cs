using Blog.Domain.core.Category.AppService;
using Blog.Domain.core.Category.DTOs;
using Blog.Domain.core.Post.AppService;
using Blog.Domain.core.Post.DTOs;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Presentation.RazorPages.Pages
{
    
    public class PostsModel(IPostAppService postAppService, ICategoryAppService categoryAppService) : PageModel
    {
        public List<CategoryDto> Categories { get; set; }
        public List<PostDto> Posts { get; set; }
        public void OnGet(int? userId)
        {
            Categories = categoryAppService.GetAllCategories();
            if (userId == null)
            {
                Posts = postAppService.GetAll();
            }
            else
            {
                Posts = postAppService.GetAllBy(userId.Value);
            }
        }
    }
}
