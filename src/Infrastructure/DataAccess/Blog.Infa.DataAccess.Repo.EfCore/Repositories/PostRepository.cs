using Blog.Domain.core.Category.DTOs;
using Blog.Domain.core.Post.Data;
using Blog.Domain.core.Post.DTOs;
using Blog.Domain.core.Post.Entities;
using Blog.Domain.core.User.Entities;
using Blog.Infa.Db.SqlServer.EfCore.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infa.DataAccess.Repo.EfCore.Repositories;

public class PostRepository(AppDbContext context) : IPostRepository
{
    public async Task<bool> CreateAsync(CreatePostDto postDto)
    {
        var post = new Post
        {
            Title = postDto.Title,
            Description = postDto.Description,
            Slug = postDto.Slug,
            CategoryId = postDto.CategoryId,
            AuthorId = postDto.AuthorId,
            Img = postDto.Img,
            SubCategoryId = postDto.SubCategoryId,
            Context = postDto.Context
        };

        await context.Posts.AddAsync(post);

        return await context.SaveChangesAsync() > 0;
    }


    public async Task<bool> EditAsync(EditPostDto postDto)
    {
        var effectedRows = context.Posts.Where(p => p.Id == postDto.PostId)
            .ExecuteUpdateAsync(setter => setter
                .SetProperty(p => p.Slug, postDto.Slug)
                .SetProperty(p => p.Title, postDto.Title)
                .SetProperty(p => p.Description, postDto.Description)
                .SetProperty(p => p.CategoryId, postDto.CategoryId)
                .SetProperty(p => p.SubCategoryId, postDto.SubCategoryId)
                .SetProperty(p => p.Img, postDto.Img)

            );
        return await effectedRows > 0;
    }

    public async Task<List<PostDto>> GetAllByAsync(int userId)
    {
        return await context.Posts.Where(p => p.AuthorId == userId)
            .Select(p => new PostDto()
            {
                Title = p.Title,
                Slug = p.Slug,
                Description = p.Description,
                CategoryId = p.CategoryId,
                PostId = p.Id,
                AuthorId = p.AuthorId,
                AuthorName = $"{p.Author.FirstName} {p.Author.LastName}",
                Img = p.Img,
                Context = p.Context,
                PostViews = p.visits,
                CreatedAt = p.CreatedAt,
                SubCategoryId = p.SubCategoryId,
            }).ToListAsync();
    }


    public async Task<List<PostDto>> GetAllAsync()
    {
        return await context.Posts
            .Select(p => new PostDto()
            {
                Title = p.Title,
                Slug = p.Slug,
                Description = p.Description,
                CategoryId = p.CategoryId,
                PostId = p.Id,
                AuthorId = p.AuthorId,
                AuthorName = $"{p.Author.FirstName} {p.Author.LastName}",
                Img = p.Img,
                Context = p.Context,
                PostViews = p.visits,
                CreatedAt = p.CreatedAt,
                SubCategoryId = p.SubCategoryId,
            }).ToListAsync();
    }


    public async Task<PostDto?> GetByAsync(int id)
    {
        return await context.Posts
            .Include(p => p.Category)
            .Include(p => p.SubCategory)
            .Include(p => p.Author)
            .Where(p => p.Id == id)
            .Select(p => new PostDto()
            {
                Title = p.Title,
                Slug = p.Slug,
                Description = p.Description,
                CategoryId = p.CategoryId,
                PostId = p.Id,
                AuthorId = p.AuthorId,
                AuthorName = $"{p.Author.FirstName} {p.Author.LastName}",
                Img = p.Img,
                Context = p.Context,
                PostViews = p.visits,
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
            }).FirstOrDefaultAsync();
    }


    public async Task<PostDto?> GetByAsync(string slug)
    {
        return await context.Posts
            .Where(p => p.Slug == slug)
            .Include(p => p.Category)
            .Include(p => p.SubCategory)
            .Include(p => p.Author)
            .Select(p => new PostDto()
            {
                Title = p.Title,
                Slug = p.Slug,
                Description = p.Description,
                CategoryId = p.CategoryId,
                AuthorName = $"{p.Author.FirstName} {p.Author.LastName}",
                PostId = p.Id,
                AuthorId = p.AuthorId,
                Context = p.Context,
                Img = p.Img,
                PostViews = p.visits,
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
            }).FirstOrDefaultAsync();
    }


    public async Task<List<PostDto>> GetRecentlyPostsAsync(int count)
    {
        return await context.Posts
            .Include(p => p.Category)
            .Include(p => p.SubCategory)
            .Include(p => p.Author)
            .OrderByDescending(p => p.CreatedAt)
            .Take(count)
            .Select(p => new PostDto()
            {
                Title = p.Title,
                Slug = p.Slug,
                Description = p.Description,
                CategoryId = p.CategoryId,
                AuthorName = $"{p.Author.FirstName} {p.Author.LastName}",
                PostId = p.Id,
                AuthorId = p.AuthorId,
                Context = p.Context,
                Img = p.Img,
                PostViews = p.visits,
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
            })
            .ToListAsync();
    }




    public async Task<bool> IsSlugExistAsync(string slug)
    {
        return await context.Posts.AnyAsync(p => p.Slug == slug);
    }


    public async Task<PostFilterDto> GetPostByFilterAsync(PostFilterParams filterParams)
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

        var pageCount = (int)Math.Ceiling(await post.CountAsync() / (double)filterParams.Take);

        return new PostFilterDto()
        {
            PageCount = pageCount,
            FilterParams = filterParams,
            Posts = await post
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
                .ToListAsync()
        };
    }


    public async Task<bool> IncreasePostViewsAsync(int postId)
    {
        var affectedRows = await context.Posts
            .Where(p => p.Id == postId)
            .ExecuteUpdateAsync(set => set
                .SetProperty(p => p.visits, p => p.visits + 1)
            );
        return affectedRows > 0;
    }


}