using Blog.Domain.core.Category.AppService;
using Blog.Domain.core.Category.DTOs;
using Blog.Presentation.RazorPages.Areas.Admin.Models.Categories;
using CodeYad_Blog.Web.Areas.Admin.Models.Categories;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Presentation.RazorPages.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController(ICategoryAppService categoryAppService) : Controller
    {

        public IActionResult Index()
        {
            var categories = categoryAppService.GetAllCategories();
            return View(categories);
        }

        [Route("/admin/category/add/{parentId?}")]
        public IActionResult Add(int? parentId)
        {
            return View();
        }

        [HttpPost("/admin/category/add/{parentId?}")]
        public IActionResult Add(int? parentId, CreateCategoryViewModel createViewModel)
        {
            createViewModel.ParentId = parentId;
            var result = categoryAppService.CreateCategory(createViewModel.MapToDto());
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(nameof(createViewModel.Slug), result.Message);
                return View();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var category = categoryAppService.GetCategoryBy(id);
            if (category == null)
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
        public IActionResult Edit(int id, EditCategoryViewModel editModel)
        {
            var result = categoryAppService.UpdateCategory(id, new EditCategoryDto()
            {
                Slug = editModel.Slug,
                MetaTag = editModel.MetaTag,
                MetaDescription = editModel.MetaDescription,
                Title = editModel.Title,
                Id = id
            });
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(nameof(editModel.Slug), result.Message);
                return View();
            }
            return RedirectToAction("Index");
        }
    }
}
