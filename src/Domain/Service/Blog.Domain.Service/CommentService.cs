using Blog.Domain.core.PostComment.Data;
using Blog.Domain.core.PostComment.DTOs;
using Blog.Domain.core.PostComment.Service;

namespace Blog.Domain.Service;

public class CommentService(ICommentRepository commentRepo) : ICommentService
{
    public async Task<bool> CreateCommentAsync(CreateCommentDto commentDto, CancellationToken cancellationToken)
    {
        return await commentRepo.CreateCommentAsync(commentDto, cancellationToken);
    }

    public async Task<List<CommentDto>> GetCommentsPostAsync(int userId, CancellationToken cancellationToken)
    {
        return await commentRepo.GetCommentsPostAsync(userId, cancellationToken);
    }

    public async Task<bool> ApproveCommentAsync(int commentId, CancellationToken cancellationToken)
    {
        return await commentRepo.ApproveCommentAsync(commentId, cancellationToken);
    }

    public async Task<bool> RejectCommentAsync(int commentId, CancellationToken cancellationToken)
    {
        return await commentRepo.RejectCommentAsync(commentId, cancellationToken);
    }

    public async Task<bool> DeleteCommentAsync(int commentId, CancellationToken cancellationToken)
    {
        return await commentRepo.DeleteCommentAsync(commentId, cancellationToken);
    }
}