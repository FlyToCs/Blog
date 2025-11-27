using Blog.Domain.core.PostComment.DTOs;

namespace Blog.Domain.core.PostComment.Service;

public interface ICommentService
{
    bool CreateComment(CreateCommentDto commentDto);
    List<CommentDto> GetCommentsPost(int userId);
    bool ApproveComment(int commentId);
    bool RejectComment(int commentId);
    bool DeleteComment(int commentId);
}