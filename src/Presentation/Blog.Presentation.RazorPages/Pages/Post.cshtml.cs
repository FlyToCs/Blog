using Blog.Domain.core.Post.AppService;
using Blog.Domain.core.Post.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Presentation.RazorPages.Pages
{
    public class PostModel(IPostAppService postAppService) : PageModel
    {
        public PostDto Post { get; set; }
        public List<PostDto> ResentlyPosts { get; set; }
        public IActionResult OnGet(string slug)
        {
            ResentlyPosts = postAppService.GetRecentlyPosts(5);
            var postResult = postAppService.GetBy(slug);
            
            if (!postResult.IsSuccess)
            {
                return NotFound();
            }
            Post = postResult.Data!;

            return Page();
        }
    }
}
