using Blog.Domain.core.Category.Data;
using Blog.Domain.core.Category.DTOs;
using Blog.Domain.core.Category.Service;

namespace Blog.Domain.Service;

public class CategoryService(ICategoryRepository categoryRepo) : ICategoryService
{
    public bool CreateCategory(CreateCategoryDto createDto)
    {
        return categoryRepo.CreateCategory(createDto);
    }

    public bool UpdateCategory(EditCategoryDto editDto)
    {
        return categoryRepo.UpdateCategory(editDto);
    }

    public List<CategoryDto> GetAllCategories()
    {
        return categoryRepo.GetAllCategories();
    }

    public CategoryDto? GetCategoryBy(int id)
    {
        return categoryRepo.GetCategoryBy(id);
    }

    public CategoryDto? GetCategoryBy(string slug)
    {
        return categoryRepo.GetCategoryBy(slug);
    }

    public bool IsSlugExist(string slug)
    {
        return categoryRepo.IsSlugExist(slug);
    }
}