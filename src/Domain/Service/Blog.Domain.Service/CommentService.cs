using Blog.Domain.core.PostComment.Data;
using Blog.Domain.core.PostComment.DTOs;
using Blog.Domain.core.PostComment.Service;

namespace Blog.Domain.Service;

public class CommentService(ICommentRepository commentRepo) : ICommentService
{
    public bool CreateComment(CreateCommentDto commentDto)
    {
        return commentRepo.CreateComment(commentDto);
    }

    public List<CommentDto> GetCommentsPost(int userId)
    {
        return commentRepo.GetCommentsPost(userId);
    }

    public bool ApproveComment(int commentId)
    {
        return commentRepo.ApproveComment(commentId);
    }

    public bool RejectComment(int commentId)
    {
        return commentRepo.RejectComment(commentId);
    }

    public bool DeleteComment(int commentId)
    {
        return commentRepo.DeleteComment(commentId);
    }
}