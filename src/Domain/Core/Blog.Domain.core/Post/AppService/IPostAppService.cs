using Blog.Domain.core._common;
using Blog.Domain.core.Post.DTOs;

namespace Blog.Domain.core.Post.AppService;

public interface IPostAppService
{
    Task<Result<bool>> CreateAsync(CreatePostDto postDto, CancellationToken cancellationToken);
    Task<Result<bool>> EditAsync(EditPostDto postDto, CancellationToken cancellationToken);
    Task<Result<PostDto>> GetByAsync(int id, CancellationToken cancellationToken);
    Task<Result<PostDto>> GetByAsync(string slug, CancellationToken cancellationToken);
    Task<PostFilterDto> GetPostsByFilterAsync(PostFilterParams filterParams, CancellationToken cancellationToken);
    Task<List<PostDto>> GetRecentlyPostsAsync(int count, CancellationToken cancellationToken);
    Task<bool> IncreasePostViewsAsync(int postId, CancellationToken cancellationToken);
    Task<List<PostDto>> GetAllByAsync(PostSearchFilter filter, CancellationToken cancellationToken);
    Task<List<PostDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<Result<bool>> DeleteAsync(int postId, CancellationToken cancellationToken);
}