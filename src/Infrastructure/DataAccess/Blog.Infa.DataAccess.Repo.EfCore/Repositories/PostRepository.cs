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
    public async Task<bool> CreateAsync(CreatePostDto postDto, CancellationToken cancellationToken)
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

        await context.Posts.AddAsync(post, cancellationToken);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }


    public async Task<bool> EditAsync(EditPostDto postDto, CancellationToken cancellationToken)
    {
        var effectedRows = context.Posts.Where(p => p.Id == postDto.PostId)
            .ExecuteUpdateAsync(setter => setter
                .SetProperty(p => p.Slug, postDto.Slug)
                .SetProperty(p => p.Title, postDto.Title)
                .SetProperty(p => p.Description, postDto.Description)
                .SetProperty(p => p.CategoryId, postDto.CategoryId)
                .SetProperty(p => p.SubCategoryId, postDto.SubCategoryId)
                .SetProperty(p => p.Img, postDto.Img), cancellationToken: cancellationToken);
        return await effectedRows > 0;
    }

    public async Task<List<PostDto>> GetAllByAsync(PostSearchFilter filter, CancellationToken cancellationToken)
    {
        var query = context.Posts.AsQueryable();
        query = query.Where(p => p.AuthorId == filter.UserId);

        if (!string.IsNullOrWhiteSpace(filter.Title))
            query = query.Where(p => p.Title.Contains(filter.Title));

        if (!string.IsNullOrWhiteSpace(filter.CategoryName))
            query = query.Where(p => p.Category.Title.Contains(filter.CategoryName));

        return await query
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
            })
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<List<PostDto>> GetAllAsync(CancellationToken cancellationToken)
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
            }).ToListAsync(cancellationToken: cancellationToken);
    }


    public async Task<PostDto?> GetByAsync(int id, CancellationToken cancellationToken)
    {
        return await context.Posts
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
                CategorySlug = p.Category.Slug,
                SubCategorySlug = p.SubCategory.Slug,
                SubCategoryTitle = p.SubCategory.Title,
                CategoryTitle = p.Category.Title
            }).FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }


    public async Task<PostDto?> GetByAsync(string slug, CancellationToken cancellationToken)
    {
        return await context.Posts
            .Where(p => p.Slug == slug)
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
                CategorySlug = p.Category.Slug,
                SubCategorySlug = p.SubCategory.Slug,
                SubCategoryTitle = p.SubCategory.Title,
                CategoryTitle = p.Category.Title
            }).FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }


    public async Task<List<PostDto>> GetRecentlyPostsAsync(int count, CancellationToken cancellationToken)
    {
        return await context.Posts
            .OrderByDescending(p => p.CreatedAt)
            .Take(count)
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
                CategorySlug = p.Category.Slug,
                SubCategorySlug = p.SubCategory.Slug,
                SubCategoryTitle = p.SubCategory.Title,
                CategoryTitle = p.Category.Title

            })
            .ToListAsync(cancellationToken: cancellationToken);
    }




    public async Task<bool> IsSlugExistAsync(string slug, CancellationToken cancellationToken)
    {
        return await context.Posts.AnyAsync(p => p.Slug == slug, cancellationToken: cancellationToken);
    }


    public async Task<PostFilterDto> GetPostByFilterAsync(PostFilterParams filterParams,
        CancellationToken cancellationToken)
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

        var pageCount = (int)Math.Ceiling(await post.CountAsync(cancellationToken: cancellationToken) / (double)filterParams.Take);

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
                    AuthorName = $"{p.Author.FirstName} {p.Author.LastName}",
                    Img = p.Img,
                    Context = p.Context,
                    PostViews = p.visits,
                    CreatedAt = p.CreatedAt,
                    SubCategoryId = p.SubCategoryId,
                    CategorySlug = p.Category.Slug,
                    SubCategorySlug = p.SubCategory.Slug,
                    SubCategoryTitle = p.SubCategory.Title,
                    CategoryTitle = p.Category.Title
                })
                .ToListAsync(cancellationToken: cancellationToken)
        };
    }


    public async Task<bool> IncreasePostViewsAsync(int postId, CancellationToken cancellationToken)
    {
        var affectedRows = await context.Posts
            .Where(p => p.Id == postId)
            .ExecuteUpdateAsync(set => set
                .SetProperty(p => p.visits, p => p.visits + 1), cancellationToken: cancellationToken);
        return affectedRows > 0;
    }

    public async Task<bool> DeleteAsync(int postId, CancellationToken cancellationToken)
    {
        var effectedRows = await context.Posts
            .Where(p => p.Id == postId)
            .ExecuteUpdateAsync(setter => setter.SetProperty(p => p.IsDeleted, true), cancellationToken: cancellationToken);
        return effectedRows > 0;
    }
}