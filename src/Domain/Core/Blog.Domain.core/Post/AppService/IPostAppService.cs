using Blog.Domain.core._common;
using Blog.Domain.core.Post.DTOs;

namespace Blog.Domain.core.Post.AppService;

public interface IPostAppService
{
    Result<bool> Create(CreatePostDto postDto);
    Result<bool> Edit(EditPostDto postDto);
    Result<PostDto> GetBy(int id);
    Result<PostDto> GetBy(string slug);
    PostFilterDto GetPostsByFilter(PostFilterParams filterParams);
    List<PostDto> GetRecentlyPosts(int count);
    bool IncreasePostViews(int postId);
    List<PostDto> GetAllBy(int userId);
    List<PostDto> GetAll();

}