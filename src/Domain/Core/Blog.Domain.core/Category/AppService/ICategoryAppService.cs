using Blog.Domain.core._common;
using Blog.Domain.core.Category.DTOs;

namespace Blog.Domain.core.Category.AppService;

public interface ICategoryAppService
{
    Task<Result<bool>> CreateCategoryAsync(CreateCategoryDto createDto);
    Task<Result<bool>> UpdateCategoryAsync(EditCategoryDto editDto);
    Task<List<CategoryDto>> GetAllCategoriesAsync();
    Task<List<CategoryDto>> GetAllCategoriesByAsync(int userId);
    Task<List<CategoryDto>> GetChildCategoriesAsync(int parentId);
    Task<Result<CategoryDto>> GetCategoryByIdAsync(int id);
    Task<Result<CategoryDto>> GetCategoryBySlugAsync(string slug);
    Task<Result<bool>> DeleteAsync(int categoryId);

}