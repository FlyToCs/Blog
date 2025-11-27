using Blog.Domain.core.Post.Data;
using Blog.Domain.core.Post.DTOs;
using Blog.Domain.core.Post.Service;

namespace Blog.Domain.Service;

public class PostService(IPostRepository postRepo) : IPostService
{
    public async Task<bool> CreateAsync(CreatePostDto postDto)
    {
        return await postRepo.CreateAsync(postDto);
    }

    public async Task<bool> EditAsync(EditPostDto postDto)
    {
        return await postRepo.EditAsync(postDto);
    }

    public async Task<PostDto?> GetByAsync(string slug)
    {
        return await postRepo.GetByAsync(slug);
    }

    public async Task<PostDto?> GetByAsync(int id)
    {
        return await postRepo.GetByAsync(id);
    }

    public async Task<bool> IsSlugExistAsync(string slug)
    {
        return await postRepo.IsSlugExistAsync(slug);
    }

    public async Task<PostFilterDto> GetPostByFilterAsync(PostFilterParams filterParams)
    {
        return await postRepo.GetPostByFilterAsync(filterParams);
    }

    public async Task<List<PostDto>> GetRecentlyPostsAsync(int count)
    {
        return await postRepo.GetRecentlyPostsAsync(count);
    }

    public async Task<bool> IncreasePostViewsAsync(int postId)
    {
        return await postRepo.IncreasePostViewsAsync(postId);
    }

    public async Task<List<PostDto>> GetAllByAsync(int userId)
    {
        return await postRepo.GetAllByAsync(userId);
    }

    public async Task<List<PostDto>> GetAllAsync()
    {
        return await postRepo.GetAllAsync();
    }
}