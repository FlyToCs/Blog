using Blog.Domain.core.Category.Data;
using Blog.Domain.core.Category.DTOs;
using Blog.Domain.core.Category.Service;

namespace Blog.Domain.Service;

public class CategoryService(ICategoryRepository categoryRepo) : ICategoryService
{
    public async Task<bool> CreateCategoryAsync(CreateCategoryDto createDto, CancellationToken cancellationToken)
    {
        return await categoryRepo.CreateCategoryAsync(createDto, cancellationToken);
    }

    public async Task<bool> UpdateCategoryAsync(EditCategoryDto editDto, CancellationToken cancellationToken)
    {
        return await categoryRepo.UpdateCategoryAsync(editDto,cancellationToken );
    }

    public async Task<List<CategoryDto>> GetAllCategoriesAsync(CancellationToken cancellationToken)
    {
        return await categoryRepo.GetAllCategoriesAsync(cancellationToken);
    }

    public async Task<List<CategoryDto>> GetAllCategoriesByAsync(int userId, CancellationToken cancellationToken)
    {
        return await categoryRepo.GetAllCategoriesByAsync(userId, cancellationToken);
    }

    public async Task<List<CategoryDto>> GetChildCategoriesAsync(int parentId, CancellationToken cancellationToken)
    {
        return await categoryRepo.GetChildCategoriesAsync(parentId, cancellationToken);
    }

    public async Task<CategoryDto?> GetCategoryByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await categoryRepo.GetCategoryByIdAsync(id, cancellationToken);
    }

    public async Task<CategoryDto?> GetCategoryBySlugAsync(string slug, CancellationToken cancellationToken)
    {
        return await categoryRepo.GetCategoryBySlugAsync(slug, cancellationToken);

    }

    public async Task<bool> IsSlugExistAsync(string slug, CancellationToken cancellationToken)
    {
        return await categoryRepo.IsSlugExistAsync(slug, cancellationToken);
    }

    public async Task<bool> DeleteAsync(int categoryId, CancellationToken cancellationToken)
    {
        return await categoryRepo.DeleteAsync(categoryId, cancellationToken);
    }
}