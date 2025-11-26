using Blog.Domain.core._common;
using Blog.Domain.core.PostComment.DTOs;

namespace Blog.Domain.core.PostComment.AppService;

public interface ICommentAppService
{
    Result<bool> CreateComment(CreateCommentDto commentDto);
    List<CommentDto> GetCommentsPost(int postId);
}