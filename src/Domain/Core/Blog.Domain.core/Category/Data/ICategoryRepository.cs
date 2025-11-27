using Blog.Domain.core.Category.DTOs;

namespace Blog.Domain.core.Category.Data;

public interface ICategoryRepository
{
    bool CreateCategory(CreateCategoryDto createDto);
    bool UpdateCategory(EditCategoryDto editDto);

    List<CategoryDto> GetAllCategories();
    List<CategoryDto> GetAllCategoriesBy(int userId);
    List<CategoryDto> GetChildCategories(int parentId);
    CategoryDto? GetCategoryBy(int id);
    CategoryDto? GetCategoryBy(string slug);
    bool IsSlugExist(string slug);
}