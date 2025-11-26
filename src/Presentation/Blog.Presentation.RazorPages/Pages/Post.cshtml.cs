using System.ComponentModel.DataAnnotations;
using Blog.Domain.core.Post.AppService;
using Blog.Domain.core.Post.DTOs;
using Blog.Domain.core.Post.Entities;
using Blog.Domain.core.PostComment.AppService;
using Blog.Domain.core.PostComment.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Blog.Presentation.RazorPages.Pages
{
    public class PostModel(IPostAppService postAppService, ICommentAppService commentAppService) : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "پر کردن این فیلد اجباری است")]
        [MinLength(4,ErrorMessage = "طول پیام حداقل باید 4 کاراکتر باشد")]
        public string Comment { get; set; }
        public List<CommentDto> Comments { get; set; }
        public PostDto Post { get; set; }
        public List<PostDto> ResentlyPosts { get; set; }


        public IActionResult OnGet(string slug)
        {
            var result = ReloadPageData(slug);

            if (result != null!)
                return result;
            postAppService.IncreasePostViews(Post.PostId);
            return Page();
        }

        public IActionResult OnPost(string slug)
        {
           
            var loadResult = ReloadPageData(slug);
            if (loadResult != null!)
                return loadResult;   

            if (!ModelState.IsValid)
                return Page();
            
            var comment = new CreateCommentDto()
            {
                PostId = Post.PostId,
                Text = Comment,
                UserId = 1
            };
            commentAppService.CreateComment(comment);
            ReloadPageData(slug);
            return Page();
        }


        private IActionResult ReloadPageData(string slug)
        {
            ResentlyPosts = postAppService.GetRecentlyPosts(5);

            var postResult = postAppService.GetBy(slug);

            if (!postResult.IsSuccess)
            {
                return NotFound();
            }

            Post = postResult.Data!;
            Comments = commentAppService.GetCommentsPost(Post.PostId);

            return null!; 
        }



    }
}
