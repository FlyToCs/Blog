using Blog.Domain.core.Category.AppService;
using Blog.Domain.core.Category.DTOs;
using Blog.Domain.core.Post.AppService;
using Blog.Domain.core.Post.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Presentation.RazorPages.Pages
{
    
    public class PostsModel(IPostAppService postAppService, ICategoryAppService categoryAppService) : PageModel
    {
        [BindProperty(SupportsGet = true)] 
        public PostSearchFilter PostSearchFilter { get; set; }
        public List<CategoryDto> Categories { get; set; }
        public List<PostDto> Posts { get; set; }
        public async Task OnGet(int? userId, CancellationToken cancellationToken)
        {
            Categories = await categoryAppService.GetAllCategoriesAsync(cancellationToken);
            if (userId == null)
            {
                Posts = await postAppService.GetAllAsync(cancellationToken);
            }
            else
            {
                Posts = await postAppService.GetAllByAsync(PostSearchFilter, cancellationToken);
            }
        }
    }
}
