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
        public async Task<IActionResult> Index(CancellationToken cancellationToken,int pageId = 1, string title = "", string categorySlug = "")
        {
            var param = new PostFilterParams()
            {
                CategorySlug = categorySlug,
                PageId = pageId,
                Take = 1,
                Title = title
            };

            var model = await postAppService.GetPostsByFilterAsync(param, cancellationToken);
            return View(model);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreatePostViewModel createViewModel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(createViewModel);
            }

            if (createViewModel.ImageFile != null)
            {
                var allowedExtensions = new[] { ".png", ".jpg", ".jpeg" };
                var extension = Path.GetExtension(createViewModel.ImageFile.FileName)?.ToLower();

                if (string.IsNullOrEmpty(extension) || !allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError(nameof(CreatePostViewModel.ImageFile), "پسوند تصویر نامعتبر است. فقط png و jpg مجاز هستند.");
                    return View(createViewModel);
                }

                var maxSize = 2 * 1024 * 1024; 
                if (createViewModel.ImageFile.Length > maxSize)
                {
                    ModelState.AddModelError(nameof(CreatePostViewModel.ImageFile), "حجم تصویر نمی‌تواند بیشتر از 2 مگابایت باشد.");
                    return View(createViewModel);
                }
            }
            else
            {
                ModelState.AddModelError(nameof(CreatePostViewModel.ImageFile), "لطفا یک تصویر انتخاب کنید.");
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
                Img = fileManager.SaveFileAndReturnName(createViewModel.ImageFile, Directories.PostImage)
            };

            var result = await postAppService.CreateAsync(postDto, cancellationToken);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError(nameof(CreatePostViewModel.Slug), result.Message);
                return View(createViewModel);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var post =await postAppService.GetByAsync(id, cancellationToken);
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
        public async Task<IActionResult> Edit(int id, EditPostViewModel editViewModel, CancellationToken cancellationToken)
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
            }, cancellationToken);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(nameof(CreatePostViewModel.Slug), result.Message);
                return View(editViewModel);
            }

            return RedirectToAction("Index");
        }
    }
}
