using Blog.Domain.core.Category.Data;
using Blog.Domain.core.Category.DTOs;
using Blog.Domain.core.Category.Entities;
using Blog.Infa.Db.SqlServer.EfCore.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infa.DataAccess.Repo.EfCore.Repositories;

public class CategoryRepository(AppDbContext context) : ICategoryRepository
{
    public bool CreateCategory(CreateCategoryDto createDto)
    {
        var category = new Category()
        {
            Title = createDto.Title,
            ParentId = createDto.ParentId,
            MetaDescription = createDto.MetaDescription,
            Slug = createDto.Slug,
            MetaTag = createDto.MetaTag
        };
        context.Add(category);
        return context.SaveChanges() > 0;
    }

    public bool UpdateCategory(EditCategoryDto editDto)
    {
        var effectedRows = context.Categories.Where(c => c.Id == editDto.Id)
            .ExecuteUpdate(setter => setter
                .SetProperty(c => c.Title, editDto.Title)
                .SetProperty(c => c.Slug, editDto.Slug)
                .SetProperty(c => c.MetaDescription, editDto.MetaDescription)
                .SetProperty(c => c.MetaTag, editDto.MetaTag)
                .SetProperty(c => c.ParentId, editDto.ParentId)
            );
        return effectedRows > 0;
    }

    public List<CategoryDto> GetAllCategories()
    {
        return context.Categories
            .Select(c => new CategoryDto()
            {
                Id = c.Id,
                Title = c.Title,
                ParentId = c.ParentId,
                Slug = c.Slug,
                MetaDescription = c.MetaDescription,
                MetaTag = c.MetaTag
            }).ToList();
    }

    public CategoryDto? GetCategoryBy(int id)
    {
        return context.Categories.Where(x=>x.Id == id)
            .Select(c => new CategoryDto()
        {
            Id = c.Id,
            Title = c.Title,
            ParentId = c.ParentId,
            Slug = c.Slug,
            MetaDescription = c.MetaDescription,
            MetaTag = c.MetaTag
        }).FirstOrDefault();
    }

    public CategoryDto? GetCategoryBy(string slug)
    {
        return context.Categories
            .Select(c => new CategoryDto()
        {
            Id = c.Id,
            Title = c.Title,
            ParentId = c.ParentId,
            Slug = c.Slug,
            MetaDescription = c.MetaDescription,
            MetaTag = c.MetaTag
        }).FirstOrDefault();
    }

    public bool IsSlugExist(string slug)
    {
       return context.Categories.Any(c => c.Slug == slug);
    }
}