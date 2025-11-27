using Blog.Domain.core.PostComment.Data;
using Blog.Domain.core.PostComment.DTOs;
using Blog.Domain.core.PostComment.Service;

namespace Blog.Domain.Service;

public class CommentService(ICommentRepository commentRepo) : ICommentService
{
    public async Task<bool> CreateCommentAsync(CreateCommentDto commentDto)
    {
        return await commentRepo.CreateCommentAsync(commentDto);
    }

    public async Task<List<CommentDto>> GetCommentsPostAsync(int userId)
    {
        return await commentRepo.GetCommentsPostAsync(userId);
    }

    public async Task<bool> ApproveCommentAsync(int commentId)
    {
        return await commentRepo.ApproveCommentAsync(commentId);
    }

    public async Task<bool> RejectCommentAsync(int commentId)
    {
        return await commentRepo.RejectCommentAsync(commentId);
    }

    public async Task<bool> DeleteCommentAsync(int commentId)
    {
        return await commentRepo.DeleteCommentAsync(commentId);
    }
}