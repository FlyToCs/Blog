using Blog.Domain.core._common;
using Blog.Domain.core.Category.AppService;
using Blog.Domain.core.Category.DTOs;
using Blog.Domain.core.Category.Service;

namespace Blog.Domain.AppService;

public class CategoryAppService(ICategoryService categoryService) : ICategoryAppService
{
    public Result<bool> CreateCategory(CreateCategoryDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
            return Result<bool>.Failure("عنوان نمیتواند خالی باشد");

        if (string.IsNullOrWhiteSpace(dto.Slug))
            return Result<bool>.Failure("اسلاگ نمیتواند خالی باشد");

        var success = categoryService.CreateCategory(dto);

        if (!success)
            return Result<bool>.Failure("ایجاد دسته بندی با خطا مواجه شد");

        return Result<bool>.Success(true, "دسته بندی با موفقیت ساخته شد");
    }

    public Result<bool> UpdateCategory(int categoryId, EditCategoryDto dto)
    {
        if (categoryId <= 0)
            return Result<bool>.Failure("آیدی دسته بندی معتبر نیست");

        if (string.IsNullOrWhiteSpace(dto.Title))
            return Result<bool>.Failure("عنوان نمیتواند خالی باشد");

        if (string.IsNullOrWhiteSpace(dto.Slug))
            return Result<bool>.Failure("اسلاگ نمیتواند خالی باشد");

        var success = categoryService.UpdateCategory(categoryId, dto);

        if (!success)
            return Result<bool>.Failure("اپدیت دسته بندی با خطا مواجه شد");

        return Result<bool>.Success(true, "دسته بندی با موفقیت اپدیت شد");
    }

    public List<CategoryDto> GetAllCategories()
    {
        return categoryService.GetAllCategories();
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