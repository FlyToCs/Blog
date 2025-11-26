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

    public List<CommentDto> GetCommentsPost(int postId)
    {
        return commentRepo.GetCommentsPost(postId);
    }
}