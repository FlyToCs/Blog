using Blog.Domain.core.Post.DTOs;

namespace Blog.Domain.core.Post.Data;

public interface IPostRepository
{
    bool Create(CreatePostDto postDto);
    bool Edit(EditPostDto postDto);
    List<PostDto> GetAllBy(int userId);
    List<PostDto> GetAll();
    PostDto? GetBy(int id);
    PostDto? GetBy(string slug);
    List<PostDto> GetRecentlyPosts(int count);
    bool IsSlugExist(string slug);
    PostFilterDto GetPostByFilter(PostFilterParams filterParams);
    bool IncreasePostViews(int postId);
}