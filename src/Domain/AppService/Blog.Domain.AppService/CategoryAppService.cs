using Blog.Domain.core._common;
using Blog.Domain.core.Category.AppService;
using Blog.Domain.core.Category.DTOs;
using Blog.Domain.core.Category.Service;
using Blog.Framework;

namespace Blog.Domain.AppService;

public class CategoryAppService(ICategoryService categoryService) : ICategoryAppService
{
    public async Task<Result<bool>> CreateCategoryAsync(CreateCategoryDto dto, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
            return Result<bool>.Failure("عنوان نمیتواند خالی باشد");

        if (string.IsNullOrWhiteSpace(dto.Slug))
            return Result<bool>.Failure("اسلاگ نمیتواند خالی باشد");

        dto.Slug = dto.Slug.ToSlug();

        var success = await categoryService.CreateCategoryAsync(dto,cancellationToken);

        if (!success)
            return Result<bool>.Failure("ایجاد دسته بندی با خطا مواجه شد");

        return Result<bool>.Success(true, "دسته بندی با موفقیت ساخته شد");
    }

    public async Task<Result<bool>> UpdateCategoryAsync(EditCategoryDto dto, CancellationToken cancellationToken)
    {
        var category = await categoryService.GetCategoryByIdAsync(dto.Id,cancellationToken);
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
            if (await categoryService.IsSlugExistAsync(dto.Slug,cancellationToken))
                return Result<bool>.Failure("این slug تکراری است");
        }

        var success = await categoryService.UpdateCategoryAsync(dto,cancellationToken);

        if (!success)
            return Result<bool>.Failure("اپدیت دسته بندی با خطا مواجه شد");

        return Result<bool>.Success(true, "دسته بندی با موفقیت اپدیت شد");
    }

    public async Task<List<CategoryDto>> GetAllCategoriesAsync(CancellationToken cancellationToken)
    {
        return await categoryService.GetAllCategoriesAsync(cancellationToken);
    }

    public async Task<List<CategoryDto>> GetAllCategoriesByAsync(int userId, CancellationToken cancellationToken)
    {
        return await categoryService.GetAllCategoriesByAsync(userId,cancellationToken);
    }

    public async Task<List<CategoryDto>> GetChildCategoriesAsync(int parentId, CancellationToken cancellationToken)
    {
        return await categoryService.GetChildCategoriesAsync(parentId,cancellationToken);
    }

    public async Task<Result<CategoryDto>> GetCategoryByIdAsync(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
            return Result<CategoryDto>.Failure("ایدی دسته بندی معتبر نیست");

        var item = await categoryService.GetCategoryByIdAsync(id,cancellationToken);

        if (item == null)
            return Result<CategoryDto>.Failure("دسته بندی یافت نشد");

        return Result<CategoryDto>.Success(item);
    }

    public async Task<Result<CategoryDto>> GetCategoryBySlugAsync(string slug, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(slug))
            return Result<CategoryDto>.Failure("اسلاگ نمیتواند خالی باشد");

        var item = await categoryService.GetCategoryBySlugAsync(slug,cancellationToken);

        if (item == null)
            return Result<CategoryDto>.Failure("دسته بندی یافت نشد");

        return Result<CategoryDto>.Success(item);
    }

    public async Task<Result<bool>> DeleteAsync(int categoryId, CancellationToken cancellationToken)
    {
        var result = await categoryService.DeleteAsync(categoryId,cancellationToken);
        if (!result)
        {
            return Result<bool>.Failure("حذف با خطا رخ داد");
        }
        return Result<bool>.Failure("حذف با موفقیت انجام شد");
    }
}