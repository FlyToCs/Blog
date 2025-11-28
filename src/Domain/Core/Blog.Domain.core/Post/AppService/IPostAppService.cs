using Blog.Domain.core._common;
using Blog.Domain.core.Post.DTOs;

namespace Blog.Domain.core.Post.AppService;

public interface IPostAppService
{
    Task<Result<bool>> CreateAsync(CreatePostDto postDto);
    Task<Result<bool>> EditAsync(EditPostDto postDto);
    Task<Result<PostDto>> GetByAsync(int id);
    Task<Result<PostDto>> GetByAsync(string slug);
    Task<PostFilterDto> GetPostsByFilterAsync(PostFilterParams filterParams);
    Task<List<PostDto>> GetRecentlyPostsAsync(int count);
    Task<bool> IncreasePostViewsAsync(int postId);
    Task<List<PostDto>> GetAllByAsync(int userId);
    Task<List<PostDto>> GetAllAsync();
    Task<Result<bool>> DeleteAsync(int postId);
}