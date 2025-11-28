using Blog.Domain.core.Category.DTOs;

namespace Blog.Domain.core.Category.Data;

public interface ICategoryRepository
{
    Task<bool> CreateCategoryAsync(CreateCategoryDto createDto,CancellationToken cancellationToken);
    Task<bool> UpdateCategoryAsync(EditCategoryDto editDto, CancellationToken cancellationToken);
    Task<List<CategoryDto>> GetAllCategoriesAsync(CancellationToken cancellationToken);
    Task<List<CategoryDto>> GetAllCategoriesByAsync(int userId, CancellationToken cancellationToken);
    Task<List<CategoryDto>> GetChildCategoriesAsync(int parentId, CancellationToken cancellationToken);
    Task<CategoryDto?> GetCategoryByIdAsync(int id, CancellationToken cancellationToken);
    Task<CategoryDto?> GetCategoryBySlugAsync(string slug, CancellationToken cancellationToken);
    Task<bool> IsSlugExistAsync(string slug, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int categoryId, CancellationToken cancellationToken);
}