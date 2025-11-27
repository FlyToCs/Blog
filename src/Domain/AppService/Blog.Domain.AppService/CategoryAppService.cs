using Blog.Domain.core._common;
using Blog.Domain.core.Category.AppService;
using Blog.Domain.core.Category.DTOs;
using Blog.Domain.core.Category.Service;
using Blog.Framework;

namespace Blog.Domain.AppService;

public class CategoryAppService(ICategoryService categoryService) : ICategoryAppService
{
    public Result<bool> CreateCategory(CreateCategoryDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
            return Result<bool>.Failure("عنوان نمیتواند خالی باشد");

        if (string.IsNullOrWhiteSpace(dto.Slug))
            return Result<bool>.Failure("اسلاگ نمیتواند خالی باشد");
        dto.Slug = dto.Slug.ToSlug();

         

        var success = categoryService.CreateCategory(dto);

        if (!success)
            return Result<bool>.Failure("ایجاد دسته بندی با خطا مواجه شد");

        return Result<bool>.Success(true, "دسته بندی با موفقیت ساخته شد");
    }

    public Result<bool> UpdateCategory(EditCategoryDto dto)
    {
        var category = categoryService.GetCategoryBy(dto.Id);
        if (category == null)
        {
            return Result<bool>.Failure("دسته بندی معتبری یافت نشد");
        }


        if (dto.Id <= 0)
            return Result<bool>.Failure("آیدی دسته بندی معتبر نیست");

        if (string.IsNullOrWhiteSpace(dto.Title))
            return Result<bool>.Failure("عنوان نمیتواند خالی باشد");

        if (string.IsNullOrWhiteSpace(dto.Slug))
            return Result<bool>.Failure("اسلاگ نمیتواند خالی باشد");
        dto.Slug = dto.Slug.ToSlug();


        if (dto.Slug !=category.Slug)
        {
            if (categoryService.IsSlugExist(dto.Slug))
            {
                return Result<bool>.Failure("این slug تکراری است");
            }
        }

        var success = categoryService.UpdateCategory(dto);

        if (!success)
            return Result<bool>.Failure("اپدیت دسته بندی با خطا مواجه شد");

        return Result<bool>.Success(true, "دسته بندی با موفقیت اپدیت شد");
    }

    public List<CategoryDto> GetAllCategories()
    {
        return categoryService.GetAllCategories();
    }

    public List<CategoryDto> GetAllCategoriesBy(int userId)
    {
        return categoryService.GetAllCategoriesBy(userId);
    }

    public List<CategoryDto> GetChildCategories(int parentId)
    {
        return categoryService.GetChildCategories(parentId);
    }

    public Result<CategoryDto> GetCategoryBy(int id)
    {
        if (id <= 0)
            return Result<CategoryDto>.Failure("ایدی دسته بندی معتبر نیست");

        var item = categoryService.GetCategoryBy(id);

        if (item == null!)
            return Result<CategoryDto>.Failure("دسته بندی یافت نشد");

        return Result<CategoryDto>.Success(item);
    }


    public Result<CategoryDto> GetCategoryBy(string slug)
    {
        if (string.IsNullOrWhiteSpace(slug))
            return Result<CategoryDto>.Failure("اسلاگ نمیتواند خالی باشد");

        var item = categoryService.GetCategoryBy(slug);

        if (item == null!)
            return Result<CategoryDto>.Failure("دسته بندی یافت نشد");

        return Result<CategoryDto>.Success(item);
    }
}