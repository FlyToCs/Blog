using Blog.Domain.core.PostComment.DTOs;

namespace Blog.Domain.core.PostComment.Data;

public interface ICommentRepository
{
    bool CreateComment(CreateCommentDto commentDto);
    List<CommentDto> GetCommentsPost(int postId);
}