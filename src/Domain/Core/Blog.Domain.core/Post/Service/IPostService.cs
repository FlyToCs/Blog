using Blog.Domain.core.Post.DTOs;

namespace Blog.Domain.core.Post.Service;

public interface IPostService
{
    bool Create(CreatePostDto postDto);
    bool Edit(EditPostDto postDto);
    PostDto? GetBy(string slug);
    PostDto? GetBy(int id);
    bool IsSlugExist(string slug);
    PostFilterDto GetPostByFilter(PostFilterParams filterParams);
    List<PostDto> GetRecentlyPosts(int count);
    bool IncreasePostViews(int postId);

}