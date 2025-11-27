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

    public List<CommentDto> GetCommentsPost(int userId)
    {
        return commentService.GetCommentsPost(userId);
    }

    public Result<bool> ApproveComment(int commentId)
    {
        var result = commentService.ApproveComment(commentId);
        if (!result)
        {
            return Result<bool>.Failure("رد کامنت با شکست مواجه شد");
        }

        return Result<bool>.Success(true,"عملیات با موفقیت انجام شد");
    }

    public Result<bool> RejectComment(int commentId)
    {
        var result = commentService.RejectComment(commentId);
        if (!result)
        {
            return Result<bool>.Failure("تایید کامنت با شکست مواجه شد");
        }

        return Result<bool>.Success(true, "عملیات با موفقیت انجام شد");
    }

    public Result<bool> DeleteComment(int commentId)
    {
        var result = commentService.DeleteComment(commentId);
        if (!result)
        {
            return Result<bool>.Failure("حذف کامنت با شکست مواجه شد");
        }

        return Result<bool>.Success(true, "عملیات با موفقیت انجام شد");
    }
}