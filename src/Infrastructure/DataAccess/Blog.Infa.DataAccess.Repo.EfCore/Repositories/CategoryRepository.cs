using Blog.Domain.core.Category.Data;
using Blog.Domain.core.Category.DTOs;
using Blog.Domain.core.Category.Entities;
using Blog.Infa.Db.SqlServer.EfCore.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infa.DataAccess.Repo.EfCore.Repositories;

public class CategoryRepository(AppDbContext context) : ICategoryRepository
{
    public async Task<bool> CreateCategoryAsync(CreateCategoryDto createDto, CancellationToken cancellationToken)
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

        await context.AddAsync(category, cancellationToken);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> UpdateCategoryAsync(EditCategoryDto editDto, CancellationToken cancellationToken)
    {
        var category = await context.Categories.FirstOrDefaultAsync(c=>c.Id == editDto.Id, cancellationToken: cancellationToken);
        if (category == null) return false;

        category.Title = editDto.Title;
        category.Slug = editDto.Slug;
        category.MetaDescription = editDto.MetaDescription;
        category.MetaTag = editDto.MetaTag;
        category.ParentId = editDto.ParentId;

        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<List<CategoryDto>> GetAllCategoriesAsync(CancellationToken cancellationToken)
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
            }).ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<List<CategoryDto>> GetAllCategoriesByAsync(int userId, CancellationToken cancellationToken)
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
            }).ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<List<CategoryDto>> GetChildCategoriesAsync(int parentId, CancellationToken cancellationToken)
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
            }).ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<CategoryDto?> GetCategoryByIdAsync(int id, CancellationToken cancellationToken)
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
            }).FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task<CategoryDto?> GetCategoryBySlugAsync(string slug, CancellationToken cancellationToken)
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
            }).FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task<bool> IsSlugExistAsync(string slug, CancellationToken cancellationToken)
    {
        return await context.Categories.AnyAsync(c => c.Slug == slug, cancellationToken: cancellationToken);
    }

    public async Task<bool> DeleteAsync(int categoryId, CancellationToken cancellationToken)
    {
        var effectedRows = await context.Categories.Where(c => c.Id == categoryId)
            .ExecuteUpdateAsync(setter => setter
                .SetProperty(c => c.IsDeleted, true), cancellationToken: cancellationToken);
        return effectedRows > 0;
    }
}