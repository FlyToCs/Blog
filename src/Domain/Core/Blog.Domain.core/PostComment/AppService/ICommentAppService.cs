using Blog.Domain.core._common;
using Blog.Domain.core.PostComment.DTOs;

namespace Blog.Domain.core.PostComment.AppService;

public interface ICommentAppService
{
    Task<Result<bool>> CreateCommentAsync(CreateCommentDto commentDto);
    Task<List<CommentDto>> GetCommentsPostAsync(int userId);
    Task<Result<bool>> ApproveCommentAsync(int commentId);
    Task<Result<bool>> RejectCommentAsync(int commentId);
    Task<Result<bool>> DeleteCommentAsync(int commentId);
}