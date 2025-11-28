using Blog.Domain.core.Post.Data;
using Blog.Domain.core.Post.DTOs;
using Blog.Domain.core.Post.Service;

namespace Blog.Domain.Service;

public class PostService(IPostRepository postRepo) : IPostService
{
    public async Task<bool> CreateAsync(CreatePostDto postDto, CancellationToken cancellationToken)
    {
        return await postRepo.CreateAsync(postDto,cancellationToken);
    }

    public async Task<bool> EditAsync(EditPostDto postDto, CancellationToken cancellationToken)
    {
        return await postRepo.EditAsync(postDto, cancellationToken);
    }

    public async Task<PostDto?> GetByAsync(string slug, CancellationToken cancellationToken)
    {
        return await postRepo.GetByAsync(slug, cancellationToken);
    }

    public async Task<PostDto?> GetByAsync(int id, CancellationToken cancellationToken)
    {
        return await postRepo.GetByAsync(id, cancellationToken);
    }

    public async Task<bool> IsSlugExistAsync(string slug, CancellationToken cancellationToken)
    {
        return await postRepo.IsSlugExistAsync(slug, cancellationToken);
    }

    public async Task<PostFilterDto> GetPostByFilterAsync(PostFilterParams filterParams,
        CancellationToken cancellationToken)
    {
        return await postRepo.GetPostByFilterAsync(filterParams, cancellationToken);
    }

    public async Task<List<PostDto>> GetRecentlyPostsAsync(int count, CancellationToken cancellationToken)
    {
        return await postRepo.GetRecentlyPostsAsync(count, cancellationToken);
    }

    public async Task<bool> IncreasePostViewsAsync(int postId, CancellationToken cancellationToken)
    {
        return await postRepo.IncreasePostViewsAsync(postId, cancellationToken);
    }

    public async Task<bool> DeleteAsync(int postId, CancellationToken cancellationToken)
    {
        return await postRepo.DeleteAsync(postId, cancellationToken);
    }

    public async Task<List<PostDto>> GetAllByAsync(PostSearchFilter filter, CancellationToken cancellationToken)
    {
        return await postRepo.GetAllByAsync(filter, cancellationToken);
    }

    public async Task<List<PostDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await postRepo.GetAllAsync(cancellationToken);
    }
}