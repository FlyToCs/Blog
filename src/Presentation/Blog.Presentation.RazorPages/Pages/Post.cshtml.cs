using Blog.Domain.core.Post.AppService;
using Blog.Domain.core.Post.DTOs;
using Blog.Domain.core.Post.Entities;
using Blog.Domain.core.PostComment.AppService;
using Blog.Domain.core.PostComment.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Blog.Domain.core.PostComment.Enums;


namespace Blog.Presentation.RazorPages.Pages
{
    public class PostModel(IPostAppService postAppService, ICommentAppService commentAppService)
        : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "پر کردن این فیلد اجباری است")]
        [MinLength(4, ErrorMessage = "طول پیام حداقل باید 4 کاراکتر باشد")]
        public string Comment { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "امتاز دهی از نون شب واجب تره")]
        public RateEnum Rate { get; set; }

        public List<CommentDto> Comments { get; set; } = new();
        public PostDto Post { get; set; } = null!;
        public List<PostDto> ResentlyPosts { get; set; } = new();


        public async Task<IActionResult> OnGetAsync(string slug, CancellationToken cancellationToken)
        {
            var result = await ReloadPageDataAsync(slug,cancellationToken);
            if (result != null)
                return result;

            await postAppService.IncreasePostViewsAsync(Post.PostId, cancellationToken);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string slug, CancellationToken cancellationToken)
        {
            var loadResult = await ReloadPageDataAsync(slug,cancellationToken);
            if (loadResult != null)
                return loadResult;

            if (!ModelState.IsValid)
                return Page();

            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
            {
                ModelState.AddModelError(string.Empty, "برای ارسال نظر باید وارد سایت شوید.");
                return Page();
            }

            var comment = new CreateCommentDto()
            {
                PostId = Post.PostId,
                Text = Comment,
                Rate = Rate,
                UserId = int.Parse(userIdString)
            };
            await commentAppService.CreateCommentAsync(comment, cancellationToken);
            await ReloadPageDataAsync(slug,cancellationToken);

            return Page();
        }


        private async Task<IActionResult?> ReloadPageDataAsync(string slug, CancellationToken cancellationToken)
        {
            ResentlyPosts = await postAppService.GetRecentlyPostsAsync(5, cancellationToken);

            var postResult = await postAppService.GetByAsync(slug, cancellationToken);

            if (!postResult.IsSuccess)
                return NotFound();

            Post = postResult.Data!;
            Comments = await commentAppService.GetCommentsPostAsync(Post.PostId, cancellationToken);

            return null;
        }
    }
}
