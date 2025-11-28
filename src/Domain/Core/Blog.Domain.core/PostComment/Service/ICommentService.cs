using Blog.Domain.core.PostComment.DTOs;

namespace Blog.Domain.core.PostComment.Service;

public interface ICommentService
{
    Task<bool> CreateCommentAsync(CreateCommentDto commentDto, CancellationToken cancellationToken);
    Task<List<CommentDto>> GetCommentsPostAsync(int userId, CancellationToken cancellationToken);
    Task<bool> ApproveCommentAsync(int commentId, CancellationToken cancellationToken);
    Task<bool> RejectCommentAsync(int commentId, CancellationToken cancellationToken);
    Task<bool> DeleteCommentAsync(int commentId, CancellationToken cancellationToken);
}