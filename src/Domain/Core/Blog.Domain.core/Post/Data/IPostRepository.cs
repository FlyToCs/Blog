using Blog.Domain.core.Post.DTOs;

namespace Blog.Domain.core.Post.Data;

public interface IPostRepository
{
    Task<bool> CreateAsync(CreatePostDto postDto);
    Task<bool> EditAsync(EditPostDto postDto);
    Task<List<PostDto>> GetAllByAsync(int userId);
    Task<List<PostDto>> GetAllAsync();
    Task<PostDto?> GetByAsync(int id);
    Task<PostDto?> GetByAsync(string slug);
    Task<List<PostDto> >GetRecentlyPostsAsync(int count);
    Task<bool> IsSlugExistAsync(string slug);
    Task<PostFilterDto> GetPostByFilterAsync(PostFilterParams filterParams);
    Task<bool> IncreasePostViewsAsync(int postId);
    Task<bool> DeleteAsync(int postId);
}