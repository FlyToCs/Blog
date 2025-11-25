using Blog.Domain.core._common;
using Blog.Domain.core.Post.DTOs;

namespace Blog.Domain.core.Post.AppService;

public interface IPostAppService
{
    Result<bool> Create(CreatePostDto postDto);
    Result<bool> Edit(EditPostDto postDto);
    Result<PostDto> GetBy(int id);
    PostFilterDto GetPostsByFilter(PostFilterParams filterParams);

}