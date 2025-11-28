using Blog.Domain.core.Category.AppService;
using Blog.Domain.core.Category.DTOs;
using Blog.Domain.core.User.Entities;
using Blog.Presentation.RazorPages.Areas.Admin.Models.Categories;
using CodeYad_Blog.Web.Areas.Admin.Models.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.Presentation.RazorPages.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Writer")]
    public class CategoryController(ICategoryAppService categoryAppService) : Controller
    {
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var categories = await categoryAppService.GetAllCategoriesByAsync(userId,cancellationToken);
            return View(categories);
        }

        [Route("/admin/category/add/{parentId?}")]
        public IActionResult Add(int? parentId)
        {
            return View();
        }

        [HttpPost("/admin/category/add/{parentId?}")]
        public async Task<IActionResult> Add(int? parentId, CreateCategoryViewModel createViewModel, CancellationToken cancellationToken)
        {
            createViewModel.UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            createViewModel.ParentId = parentId;

            var result = await categoryAppService.CreateCategoryAsync(createViewModel.MapToDto(),cancellationToken);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(nameof(createViewModel.Slug), result.Message);
                return View(createViewModel);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var category = await categoryAppService.GetCategoryByIdAsync(id,cancellationToken);
            if (!category.IsSuccess || category.Data == null)
                return RedirectToAction("Index");

            var model = new EditCategoryViewModel()
            {
                Slug = category.Data.Slug,
                MetaTag = category.Data.MetaTag,
                MetaDescription = category.Data.MetaDescription,
                Title = category.Data.Title
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditCategoryViewModel editModel, CancellationToken cancellationToken)
        {
            var result = await categoryAppService.UpdateCategoryAsync(new EditCategoryDto()
            {
                Id = id,
                Slug = editModel.Slug,
                MetaTag = editModel.MetaTag,
                MetaDescription = editModel.MetaDescription,
                Title = editModel.Title
            },cancellationToken);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError(nameof(editModel.Slug), result.Message);
                return View(editModel);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> GetChildCategories(int parentId, CancellationToken cancellationToken)
        {
            var categories = await categoryAppService.GetChildCategoriesAsync(parentId,cancellationToken);
            return new JsonResult(categories);
        }
    }
}
