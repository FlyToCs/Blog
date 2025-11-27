using Blog.Domain.core._common;
using Blog.Domain.core.Category.AppService;
using Blog.Domain.core.Category.DTOs;
using Blog.Domain.core.Category.Service;
using Blog.Framework;

namespace Blog.Domain.AppService;

public class CategoryAppService(ICategoryService categoryService) : ICategoryAppService
{
    public async Task<Result<bool>> CreateCategoryAsync(CreateCategoryDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
            return Result<bool>.Failure("عنوان نمیتواند خالی باشد");

        if (string.IsNullOrWhiteSpace(dto.Slug))
            return Result<bool>.Failure("اسلاگ نمیتواند خالی باشد");

        dto.Slug = dto.Slug.ToSlug();

        var success = await categoryService.CreateCategoryAsync(dto);

        if (!success)
            return Result<bool>.Failure("ایجاد دسته بندی با خطا مواجه شد");

        return Result<bool>.Success(true, "دسته بندی با موفقیت ساخته شد");
    }

    public async Task<Result<bool>> UpdateCategoryAsync(EditCategoryDto dto)
    {
        var category = await categoryService.GetCategoryByIdAsync(dto.Id);
        if (category == null)
            return Result<bool>.Failure("دسته بندی معتبری یافت نشد");

        if (dto.Id <= 0)
            return Result<bool>.Failure("آیدی دسته بندی معتبر نیست");

        if (string.IsNullOrWhiteSpace(dto.Title))
            return Result<bool>.Failure("عنوان نمیتواند خالی باشد");

        if (string.IsNullOrWhiteSpace(dto.Slug))
            return Result<bool>.Failure("اسلاگ نمیتواند خالی باشد");

        dto.Slug = dto.Slug.ToSlug();

        if (dto.Slug != category.Slug)
        {
            if (await categoryService.IsSlugExistAsync(dto.Slug))
                return Result<bool>.Failure("این slug تکراری است");
        }

        var success = await categoryService.UpdateCategoryAsync(dto);

        if (!success)
            return Result<bool>.Failure("اپدیت دسته بندی با خطا مواجه شد");

        return Result<bool>.Success(true, "دسته بندی با موفقیت اپدیت شد");
    }

    public async Task<List<CategoryDto>> GetAllCategoriesAsync()
    {
        return await categoryService.GetAllCategoriesAsync();
    }

    public async Task<List<CategoryDto>> GetAllCategoriesByAsync(int userId)
    {
        return await categoryService.GetAllCategoriesByAsync(userId);
    }

    public async Task<List<CategoryDto>> GetChildCategoriesAsync(int parentId)
    {
        return await categoryService.GetChildCategoriesAsync(parentId);
    }

    public async Task<Result<CategoryDto>> GetCategoryByIdAsync(int id)
    {
        if (id <= 0)
            return Result<CategoryDto>.Failure("ایدی دسته بندی معتبر نیست");

        var item = await categoryService.GetCategoryByIdAsync(id);

        if (item == null)
            return Result<CategoryDto>.Failure("دسته بندی یافت نشد");

        return Result<CategoryDto>.Success(item);
    }

    public async Task<Result<CategoryDto>> GetCategoryBySlugAsync(string slug)
    {
        if (string.IsNullOrWhiteSpace(slug))
            return Result<CategoryDto>.Failure("اسلاگ نمیتواند خالی باشد");

        var item = await categoryService.GetCategoryBySlugAsync(slug);

        if (item == null)
            return Result<CategoryDto>.Failure("دسته بندی یافت نشد");

        return Result<CategoryDto>.Success(item);
    }

}