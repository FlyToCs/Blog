using Blog.Domain.core._common;
using Blog.Domain.core.Category.DTOs;

namespace Blog.Domain.core.Category.AppService;

public interface ICategoryAppService
{
    Result<bool> CreateCategory(CreateCategoryDto createDto);
    Result<bool> UpdateCategory(EditCategoryDto editDto);
    List<CategoryDto> GetAllCategories();
    Result<CategoryDto> GetCategoryBy(int id);
    Result<CategoryDto> GetCategoryBy(string slug);
}