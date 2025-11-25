using Blog.Domain.core.Post.DTOs;

namespace Blog.Domain.core.Post.Data;

public interface IPostRepository
{
    bool Create(CreatePostDto postDto);
    bool Edit(EditPostDto postDto);
    PostDto? GetBy(int id);
    bool IsSlugExist(string slug);
    PostFilterDto GetPostByFilter(PostFilterParams filterParams);
}