using Blog.Domain.core.Category.Data;
using Blog.Domain.core.Category.DTOs;
using Blog.Domain.core.Category.Entities;
using Blog.Infa.Db.SqlServer.EfCore.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infa.DataAccess.Repo.EfCore.Repositories;

public class CategoryRepository(AppDbContext context) : ICategoryRepository
{
    public async Task<bool> CreateCategoryAsync(CreateCategoryDto createDto)
    {
        var category = new Category()
        {
            Title = createDto.Title,
            ParentId = createDto.ParentId,
            MetaDescription = createDto.MetaDescription,
            Slug = createDto.Slug,
            MetaTag = createDto.MetaTag,
            UserId = createDto.UserId
        };

        await context.AddAsync(category);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateCategoryAsync(EditCategoryDto editDto)
    {
        var category = await context.Categories.FindAsync(editDto.Id);
        if (category == null) return false;

        category.Title = editDto.Title;
        category.Slug = editDto.Slug;
        category.MetaDescription = editDto.MetaDescription;
        category.MetaTag = editDto.MetaTag;
        category.ParentId = editDto.ParentId;

        return await context.SaveChangesAsync() > 0;
    }

    public async Task<List<CategoryDto>> GetAllCategoriesAsync()
    {
        return await context.Categories
            .Select(c => new CategoryDto()
            {
                Id = c.Id,
                Title = c.Title,
                ParentId = c.ParentId,
                Slug = c.Slug,
                MetaDescription = c.MetaDescription,
                MetaTag = c.MetaTag,
                UserId = c.UserId
            }).ToListAsync();
    }

    public async Task<List<CategoryDto>> GetAllCategoriesByAsync(int userId)
    {
        return await context.Categories
            .Where(c => c.UserId == userId)
            .Select(c => new CategoryDto()
            {
                Id = c.Id,
                Title = c.Title,
                ParentId = c.ParentId,
                Slug = c.Slug,
                MetaDescription = c.MetaDescription,
                MetaTag = c.MetaTag,
                UserId = c.UserId
            }).ToListAsync();
    }

    public async Task<List<CategoryDto>> GetChildCategoriesAsync(int parentId)
    {
        return await context.Categories
            .Where(c => c.ParentId == parentId)
            .Select(c => new CategoryDto()
            {
                Id = c.Id,
                Title = c.Title,
                ParentId = c.ParentId,
                Slug = c.Slug,
                MetaDescription = c.MetaDescription,
                MetaTag = c.MetaTag,
                UserId = c.UserId
            }).ToListAsync();
    }

    public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
    {
        return await context.Categories
            .Where(c => c.Id == id)
            .Select(c => new CategoryDto()
            {
                Id = c.Id,
                Title = c.Title,
                ParentId = c.ParentId,
                Slug = c.Slug,
                MetaDescription = c.MetaDescription,
                MetaTag = c.MetaTag,
                UserId = c.UserId
            }).FirstOrDefaultAsync();
    }

    public async Task<CategoryDto?> GetCategoryBySlugAsync(string slug)
    {
        return await context.Categories
            .Where(c => c.Slug == slug)
            .Select(c => new CategoryDto()
            {
                Id = c.Id,
                Title = c.Title,
                ParentId = c.ParentId,
                Slug = c.Slug,
                MetaDescription = c.MetaDescription,
                MetaTag = c.MetaTag,
                UserId = c.UserId
            }).FirstOrDefaultAsync();
    }

    public async Task<bool> IsSlugExistAsync(string slug)
    {
        return await context.Categories.AnyAsync(c => c.Slug == slug);
    }

    public async Task<bool> DeleteAsync(int categoryId)
    {
        var effectedRows = await context.Categories.Where(c => c.Id == categoryId)
            .ExecuteUpdateAsync(setter => setter
                .SetProperty(c => c.IsDeleted, true));
        return effectedRows > 0;
    }
}