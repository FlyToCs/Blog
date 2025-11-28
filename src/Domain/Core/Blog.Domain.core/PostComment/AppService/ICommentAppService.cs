using Blog.Domain.core._common;
using Blog.Domain.core.PostComment.DTOs;

namespace Blog.Domain.core.PostComment.AppService;

public interface ICommentAppService
{
    Task<Result<bool>> CreateCommentAsync(CreateCommentDto commentDto, CancellationToken cancellationToken);
    Task<List<CommentDto>> GetCommentsPostAsync(int userId, CancellationToken cancellationToken);
    Task<Result<bool>> ApproveCommentAsync(int commentId, CancellationToken cancellationToken);
    Task<Result<bool>> RejectCommentAsync(int commentId, CancellationToken cancellationToken);
    Task<Result<bool>> DeleteCommentAsync(int commentId, CancellationToken cancellationToken);
}