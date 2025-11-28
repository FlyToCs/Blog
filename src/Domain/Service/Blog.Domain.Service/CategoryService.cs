using Blog.Domain.core.Category.Data;
using Blog.Domain.core.Category.DTOs;
using Blog.Domain.core.Category.Service;

namespace Blog.Domain.Service;

public class CategoryService(ICategoryRepository categoryRepo) : ICategoryService
{
    public async Task<bool> CreateCategoryAsync(CreateCategoryDto createDto)
    {
        return await categoryRepo.CreateCategoryAsync(createDto);
    }

    public async Task<bool> UpdateCategoryAsync(EditCategoryDto editDto)
    {
        return await categoryRepo.UpdateCategoryAsync(editDto);
    }

    public async Task<List<CategoryDto>> GetAllCategoriesAsync()
    {
        return await categoryRepo.GetAllCategoriesAsync();
    }

    public async Task<List<CategoryDto>> GetAllCategoriesByAsync(int userId)
    {
        return await categoryRepo.GetAllCategoriesByAsync(userId);
    }

    public async Task<List<CategoryDto>> GetChildCategoriesAsync(int parentId)
    {
        return await categoryRepo.GetChildCategoriesAsync(parentId);
    }

    public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
    {
        return await categoryRepo.GetCategoryByIdAsync(id);
    }

    public async Task<CategoryDto?> GetCategoryBySlugAsync(string slug)
    {
        return await categoryRepo.GetCategoryBySlugAsync(slug);
    }

    public async Task<bool> IsSlugExistAsync(string slug)
    {
        return await categoryRepo.IsSlugExistAsync(slug);
    }

    public async Task<bool> DeleteAsync(int categoryId)
    {
        return await categoryRepo.DeleteAsync(categoryId);
    }
}