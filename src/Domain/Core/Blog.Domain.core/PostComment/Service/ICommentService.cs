using Blog.Domain.core.PostComment.DTOs;

namespace Blog.Domain.core.PostComment.Service;

public interface ICommentService
{
    Task<bool> CreateCommentAsync(CreateCommentDto commentDto);
    Task<List<CommentDto>> GetCommentsPostAsync(int userId);
    Task<bool> ApproveCommentAsync(int commentId);
    Task<bool> RejectCommentAsync(int commentId);
    Task<bool> DeleteCommentAsync(int commentId);
}