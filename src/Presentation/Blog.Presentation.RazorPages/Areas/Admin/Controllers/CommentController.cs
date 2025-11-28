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
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var authorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var comments = await commentAppService.GetCommentsPostAsync(authorId, cancellationToken);
            return View(comments);
        }

        [HttpGet]
        public async Task<IActionResult> ApproveComment(int id, CommentStatus status, CancellationToken cancellationToken)
        {
          
            await commentAppService.ApproveCommentAsync(id, cancellationToken);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> RejectComment(int id, CancellationToken cancellationToken)
        {

            await commentAppService.ApproveCommentAsync(id, cancellationToken);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
           await commentAppService.DeleteCommentAsync(id, cancellationToken);
            return RedirectToAction("Index");
        }
    }
}
