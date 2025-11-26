using Blog.Domain.core._common;
using Blog.Domain.core.PostComment.AppService;
using Blog.Domain.core.PostComment.DTOs;
using Blog.Domain.core.PostComment.Service;

namespace Blog.Domain.AppService;

public class CommentAppService(ICommentService commentService) : ICommentAppService
{
    public Result<bool> CreateComment(CreateCommentDto commentDto)
    {

        //validation
        var result = commentService.CreateComment(commentDto);
        if (!result)
        {
            return Result<bool>.Failure("ارسال کامنت با شکست مواجه شد");
        }

        return Result<bool>.Failure("کامت با موفقیت ارسال شد");

    }

    public List<CommentDto> GetCommentsPost(int postId)
    {
        return commentService.GetCommentsPost(postId);
    }
}