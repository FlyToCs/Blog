using Blog.Domain.core.PostComment.DTOs;

namespace Blog.Domain.core.PostComment.Service;

public interface ICommentService
{
    bool CreateComment(CreateCommentDto commentDto);
    List<CommentDto> GetCommentsPost(int postId);
}