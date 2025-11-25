using Blog.Domain.core.Post.AppService;
using Blog.Domain.core.Post.DTOs;
using Blog.Presentation.RazorPages.Areas.Admin.Models.Posts;
using Blog.Presentation.RazorPages.Services;
using Blog.Presentation.RazorPages.Services.FileManager;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Presentation.RazorPages.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController(IPostAppService postAppService, IFileManager fileManager) : Controller
    {
        public IActionResult Index(int pageId = 1, string title = "", string categorySlug = "")
        {
            var param = new PostFilterParams()
            {
                CategorySlug = categorySlug,
                PageId = pageId,
                Take = 1,
                Title = title
            };
            var model = postAppService.GetPostsByFilter(param);
            return View(model);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(CreatePostViewModel createViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(createViewModel);
            }
            var result = postAppService.Create(new CreatePostDto()
            {
                CategoryId = createViewModel.CategoryId,
                Description = createViewModel.Description,
                Img = fileManager.SaveFileAndReturnName(createViewModel.ImageFile, Directories.PostImage),
                Slug = createViewModel.Slug,
                SubCategoryId = createViewModel.SubCategoryId == 0 ? null : createViewModel.SubCategoryId,
                Title = createViewModel.Title,
                AuthorId = 5
            });

            if (!result.IsSuccess)
            {
                ModelState.AddModelError(nameof(CreatePostViewModel.Slug), result.Message);
                return View(createViewModel);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var post = postAppService.GetBy(id);
            if (post == null)
                return RedirectToAction("Index");

            var model = new EditPostViewModel()
            {
                CategoryId = post.Data.CategoryId,
                Description = post.Data.Description,
                Slug = post.Data.Slug,
                SubCategoryId = post.Data.SubCategoryId,
                Title = post.Data.Title
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EditPostViewModel editViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(editViewModel);
            }

            string newImgName = string.Empty;
            if (editViewModel.ImageFile != null!)
            {
                newImgName = fileManager.SaveFileAndReturnName(editViewModel.ImageFile, Directories.PostImage);
            }

            var result = postAppService.Edit(new EditPostDto()
            {
                CategoryId = editViewModel.CategoryId,
                Description = editViewModel.Description,
                Img = newImgName,
                Slug = editViewModel.Slug,
                SubCategoryId = editViewModel.SubCategoryId == 0 ? null : editViewModel.SubCategoryId,
                Title = editViewModel.Title,
                PostId = id
            });
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(nameof(CreatePostViewModel.Slug), result.Message);
                return View(editViewModel);
            }

            return RedirectToAction("Index");
        }
    }
}
