using Blog.Domain.core.Category.DTOs;

namespace Blog.Domain.core.Category.Data;

public interface ICategoryRepository
{
    bool CreateCategory(CreateCategoryDto createDto);
    bool UpdateCategory(int categoryId, EditCategoryDto editDto);
    List<CategoryDto> GetAllCategories();
    CategoryDto? GetCategoryBy(int id);
    CategoryDto? GetCategoryBy(string slug);

}