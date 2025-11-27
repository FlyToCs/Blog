using Blog.Domain.core.Post.AppService;
using Blog.Domain.core.Post.DTOs;
using Blog.Presentation.RazorPages.Areas.Admin.Models.Posts;
using Blog.Presentation.RazorPages.Services.FileManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.Presentation.RazorPages.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Writer")]
    public class PostController(IPostAppService postAppService, IFileManager fileManager) : Controller
    {
        public async Task<IActionResult> Index(int pageId = 1, string title = "", string categorySlug = "")
        {
            var param = new PostFilterParams()
            {
                CategorySlug = categorySlug,
                PageId = pageId,
                Take = 1,
                Title = title
            };

            var model = await postAppService.GetPostsByFilterAsync(param);
            return View(model);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreatePostViewModel createViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(createViewModel);
            }

            var postDto = new CreatePostDto
            {
                Title = createViewModel.Title,
                Description = createViewModel.Description,
                Slug = createViewModel.Slug,
                CategoryId = createViewModel.CategoryId,
                SubCategoryId = createViewModel.SubCategoryId == 0 ? null : createViewModel.SubCategoryId,
                AuthorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!),
                Context = createViewModel.Context,
                Img = fileManager.SaveFileAndReturnName(createViewModel.ImageFile,Directories.PostImage)
            };

            var result = await postAppService.CreateAsync(postDto);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError(nameof(CreatePostViewModel.Slug), result.Message);
                return View(createViewModel);
            }

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            var post =await postAppService.GetByAsync(id);
            if (post == null)
                return RedirectToAction("Index");

            var model = new EditPostViewModel()
            {
                CategoryId = post.Data.CategoryId,
                Description = post.Data.Description,
                Context = post.Data.Context,
                Slug = post.Data.Slug,
                SubCategoryId = post.Data.SubCategoryId,
                Title = post.Data.Title,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditPostViewModel editViewModel)
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

            var result =await postAppService.EditAsync(new EditPostDto()
            {
                CategoryId = editViewModel.CategoryId,
                Description = editViewModel.Description,
                Context = editViewModel.Context,
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
