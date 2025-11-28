using Blog.Domain.core.Category.DTOs;

namespace Blog.Domain.core.Category.Data;

public interface ICategoryRepository
{
    Task<bool> CreateCategoryAsync(CreateCategoryDto createDto);
    Task<bool> UpdateCategoryAsync(EditCategoryDto editDto);
    Task<List<CategoryDto>> GetAllCategoriesAsync();
    Task<List<CategoryDto>> GetAllCategoriesByAsync(int userId);
    Task<List<CategoryDto>> GetChildCategoriesAsync(int parentId);
    Task<CategoryDto?> GetCategoryByIdAsync(int id);
    Task<CategoryDto?> GetCategoryBySlugAsync(string slug);
    Task<bool> IsSlugExistAsync(string slug);
    Task<bool> DeleteAsync(int categoryId);
}