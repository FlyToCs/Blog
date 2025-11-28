using Blog.Domain.core._common;
using Blog.Domain.core.Category.DTOs;

namespace Blog.Domain.core.Category.AppService;

public interface ICategoryAppService
{
    Task<Result<bool>> CreateCategoryAsync(CreateCategoryDto createDto, CancellationToken cancellationToken);
    Task<Result<bool>> UpdateCategoryAsync(EditCategoryDto editDto, CancellationToken cancellationToken);
    Task<List<CategoryDto>> GetAllCategoriesAsync(CancellationToken cancellationToken);
    Task<List<CategoryDto>> GetAllCategoriesByAsync(int userId, CancellationToken cancellationToken);
    Task<List<CategoryDto>> GetChildCategoriesAsync(int parentId, CancellationToken cancellationToken);
    Task<Result<CategoryDto>> GetCategoryByIdAsync(int id, CancellationToken cancellationToken);
    Task<Result<CategoryDto>> GetCategoryBySlugAsync(string slug, CancellationToken cancellationToken);
    Task<Result<bool>> DeleteAsync(int categoryId, CancellationToken cancellationToken);

}