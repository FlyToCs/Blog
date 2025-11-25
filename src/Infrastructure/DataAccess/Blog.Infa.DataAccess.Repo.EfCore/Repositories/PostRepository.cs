using Blog.Domain.core.Category.DTOs;
using Blog.Domain.core.Post.Data;
using Blog.Domain.core.Post.DTOs;
using Blog.Domain.core.Post.Entities;
using Blog.Infa.Db.SqlServer.EfCore.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infa.DataAccess.Repo.EfCore.Repositories;

public class PostRepository(AppDbContext context) : IPostRepository
{
    public bool Create(CreatePostDto postDto)
    {
        var post = new Post()
        {
            Title = postDto.Title,
            Description = postDto.Description,
            Slug = postDto.Slug,
            CategoryId = postDto.CategoryId,
            AuthorId = postDto.AuthorId,
            Img = postDto.Img,
            SubCategoryId = postDto.SubCategoryId
        };
        context.Add(post);
        return context.SaveChanges() > 0;
    }

    public bool Edit(EditPostDto postDto)
    {
        var effectedRows = context.Posts.Where(p => p.Id == postDto.PostId)
            .ExecuteUpdate(setter => setter
                .SetProperty(p => p.Slug, postDto.Slug)
                .SetProperty(p => p.Title, postDto.Title)
                .SetProperty(p => p.Description, postDto.Description)
                .SetProperty(p => p.CategoryId, postDto.CategoryId)
                .SetProperty(p => p.SubCategoryId, postDto.SubCategoryId)
                .SetProperty(p => p.Img, postDto.Img)

            );
        return effectedRows > 0;
    }

    public PostDto? GetBy(int id)
    {
        return context.Posts
            .Include(p => p.Category)
            .Include(p => p.SubCategory)
            .Where(p => p.Id == id)
            .Select(p => new PostDto()
            {
                Title = p.Title,
                Slug = p.Slug,
                Description = p.Description,
                CategoryId = p.CategoryId,
                PostId = p.Id,
                AuthorId = p.AuthorId,
                Img = p.Img,
                CreatedAt = p.CreatedAt,
                SubCategoryId = p.SubCategoryId,

                Category = new CategoryDto()
                {
                    Id = p.Category.Id,
                    MetaDescription = p.Category.MetaDescription,
                    MetaTag = p.Category.MetaTag,
                    ParentId = p.Category.ParentId,
                    Slug = p.Category.Slug,
                    Title = p.Category.Title
                },

                SubCategory = p.SubCategory == null ? null : new CategoryDto()
                {
                    Id = p.SubCategory.Id,
                    MetaDescription = p.SubCategory.MetaDescription,
                    MetaTag = p.SubCategory.MetaTag,
                    ParentId = p.SubCategory.ParentId,
                    Slug = p.SubCategory.Slug,
                    Title = p.SubCategory.Title
                }
            }).FirstOrDefault();
    }


    public bool IsSlugExist(string slug)
    {
        return context.Posts.Any(p => p.Slug == slug);
    }

    public PostFilterDto GetPostByFilter(PostFilterParams filterParams)
    {
        var post = context.Posts
            .Include(p => p.Category)
            .Include(p => p.SubCategory)
            .OrderByDescending(p => p.CreatedAt)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filterParams.Title))
            post = post.Where(p => p.Title.Contains(filterParams.Title));

        if (!string.IsNullOrWhiteSpace(filterParams.CategorySlug))
            post = post.Where(p => p.Category.Slug == filterParams.CategorySlug);

        var skip = (filterParams.PageId - 1) * filterParams.Take;

        var pageCount = (int)Math.Ceiling(post.Count() / (double)filterParams.Take);


        return new PostFilterDto()
        {
            PageCount = pageCount,
            FilterParams = filterParams,
            Posts = post
                .Skip(skip)
                .Take(filterParams.Take)
                .Select(p => new PostDto()
                {
                    Title = p.Title,
                    Slug = p.Slug,
                    Description = p.Description,
                    CategoryId = p.CategoryId,
                    PostId = p.Id,
                    AuthorId = p.AuthorId,
                    Img = p.Img,
                    CreatedAt = p.CreatedAt,
                    SubCategoryId = p.SubCategoryId,

                    Category = p.Category == null ? null : new CategoryDto()
                    {
                        Id = p.CategoryId,
                        MetaDescription = p.Category.MetaDescription,
                        MetaTag = p.Category.MetaTag,
                        ParentId = p.Category.ParentId,
                        Slug = p.Category.Slug,
                        Title = p.Category.Title
                    },

                    SubCategory = p.SubCategory == null ? null : new CategoryDto()
                    {
                        Id = p.SubCategoryId.Value,
                        MetaDescription = p.SubCategory.MetaDescription,
                        MetaTag = p.SubCategory.MetaTag,
                        ParentId = p.SubCategory.ParentId,
                        Slug = p.SubCategory.Slug,
                        Title = p.SubCategory.Title
                    }
                })
                .ToList()
        };
    }

}