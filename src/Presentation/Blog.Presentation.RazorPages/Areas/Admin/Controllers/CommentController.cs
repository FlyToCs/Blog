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
        public async Task<IActionResult> Index()
        {
            var authorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var comments = await commentAppService.GetCommentsPostAsync(authorId);
            return View(comments);
        }

        [HttpGet]
        public async Task<IActionResult> ApproveComment(int id, CommentStatus status)
        {
          
            await commentAppService.ApproveCommentAsync(id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> RejectComment(int id)
        {

            await commentAppService.ApproveCommentAsync(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
           await commentAppService.DeleteCommentAsync(id);
            return RedirectToAction("Index");
        }
    }
}
