using Blog.Domain.core.Post.AppService;
using Blog.Domain.core.Post.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Presentation.RazorPages.Pages
{
    
    public class PostsModel(IPostAppService postAppService) : PageModel
    {
        
        public List<PostDto> Posts { get; set; }
        public void OnGet(int? userId)
        {
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
