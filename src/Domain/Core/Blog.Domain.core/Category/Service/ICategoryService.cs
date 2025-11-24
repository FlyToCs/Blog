using Blog.Domain.core.Category.DTOs;

namespace Blog.Domain.core.Category.Service;

public interface ICategoryService
{
    bool CreateCategory(CreateCategoryDto createDto);
    bool UpdateCategory(EditCategoryDto editDto);
    List<CategoryDto> GetAllCategories();
    CategoryDto? GetCategoryBy(int id);
    CategoryDto? GetCategoryBy(string slug);
    bool IsSlugExist(string slug);
}