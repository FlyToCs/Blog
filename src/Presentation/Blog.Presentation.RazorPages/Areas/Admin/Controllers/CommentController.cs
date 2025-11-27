using Blog.Domain.core.PostComment.AppService;
using Blog.Domain.core.PostComment.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.Presentation.RazorPages.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Admin,Writer")]
    public class CommentController(ICommentAppService commentAppService) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var authorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var comments = commentAppService.GetCommentsPost(authorId);
            return View(comments);
        }

        [HttpGet]
        public IActionResult ApproveComment(int id, CommentStatus status)
        {
          
            commentAppService.ApproveComment(id);
            return RedirectToAction("Index");
        }
        public IActionResult RejectComment(int id)
        {

            commentAppService.ApproveComment(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            commentAppService.DeleteComment(id);
            return RedirectToAction("Index");
        }
    }
}
