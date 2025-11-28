using Blog.Domain.core.Post.DTOs;

namespace Blog.Domain.core.Post.Service;

public interface IPostService
{
    Task<bool> CreateAsync(CreatePostDto postDto, CancellationToken cancellationToken);
    Task<bool> EditAsync(EditPostDto postDto, CancellationToken cancellationToken);
    Task<List<PostDto>> GetAllByAsync(PostSearchFilter filter, CancellationToken cancellationToken);
    Task<List<PostDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<PostDto?> GetByAsync(int id, CancellationToken cancellationToken);
    Task<PostDto?> GetByAsync(string slug, CancellationToken cancellationToken);
    Task<List<PostDto>> GetRecentlyPostsAsync(int count, CancellationToken cancellationToken);
    Task<bool> IsSlugExistAsync(string slug, CancellationToken cancellationToken);
    Task<PostFilterDto> GetPostByFilterAsync(PostFilterParams filterParams, CancellationToken cancellationToken);
    Task<bool> IncreasePostViewsAsync(int postId, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int postId, CancellationToken cancellationToken);

}