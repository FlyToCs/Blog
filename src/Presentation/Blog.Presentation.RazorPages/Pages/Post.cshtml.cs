using Blog.Domain.core.Post.AppService;
using Blog.Domain.core.Post.DTOs;
using Blog.Domain.core.Post.Entities;
using Blog.Domain.core.PostComment.AppService;
using Blog.Domain.core.PostComment.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;


namespace Blog.Presentation.RazorPages.Pages
{
    public class PostModel(IPostAppService postAppService, ICommentAppService commentAppService)
        : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "پر کردن این فیلد اجباری است")]
        [MinLength(4, ErrorMessage = "طول پیام حداقل باید 4 کاراکتر باشد")]
        public string Comment { get; set; } = string.Empty;

        public List<CommentDto> Comments { get; set; } = new();
        public PostDto Post { get; set; } = null!;
        public List<PostDto> ResentlyPosts { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(string slug)
        {
            var result = await ReloadPageDataAsync(slug);
            if (result != null)
                return result;

            await postAppService.IncreasePostViewsAsync(Post.PostId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string slug)
        {
            var loadResult = await ReloadPageDataAsync(slug);
            if (loadResult != null)
                return loadResult;

            if (!ModelState.IsValid)
                return Page();

            var comment = new CreateCommentDto()
            {
                PostId = Post.PostId,
                Text = Comment,
                UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)
            };

            await commentAppService.CreateCommentAsync(comment);
            await ReloadPageDataAsync(slug);
            return Page();
        }

        private async Task<IActionResult?> ReloadPageDataAsync(string slug)
        {
            ResentlyPosts = await postAppService.GetRecentlyPostsAsync(5);

            var postResult = await postAppService.GetByAsync(slug);

            if (!postResult.IsSuccess)
                return NotFound();

            Post = postResult.Data!;
            Comments = await commentAppService.GetCommentsPostAsync(Post.PostId);

            return null;
        }
    }
}
