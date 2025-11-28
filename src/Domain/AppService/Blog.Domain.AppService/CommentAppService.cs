using Blog.Domain.core._common;
using Blog.Domain.core.PostComment.AppService;
using Blog.Domain.core.PostComment.DTOs;
using Blog.Domain.core.PostComment.Service;

namespace Blog.Domain.AppService;

public class CommentAppService(ICommentService commentService) : ICommentAppService
{
    public async Task<Result<bool>> CreateCommentAsync(CreateCommentDto commentDto, CancellationToken cancellationToken)
    {
 
        var result = await commentService.CreateCommentAsync(commentDto, cancellationToken);
        if (!result)
        {
            return Result<bool>.Failure("ارسال کامنت با شکست مواجه شد");
        }

        return Result<bool>.Success(true, "کامنت با موفقیت ارسال شد");
    }

    public async Task<List<CommentDto>> GetCommentsPostAsync(int userId, CancellationToken cancellationToken)
    {
        return await commentService.GetCommentsPostAsync(userId, cancellationToken);
    }

    public async Task<Result<bool>> ApproveCommentAsync(int commentId, CancellationToken cancellationToken)
    {
        var result = await commentService.ApproveCommentAsync(commentId, cancellationToken);
        if (!result)
        {
            return Result<bool>.Failure("تایید کامنت با شکست مواجه شد");
        }

        return Result<bool>.Success(true, "عملیات با موفقیت انجام شد");
    }

    public async Task<Result<bool>> RejectCommentAsync(int commentId, CancellationToken cancellationToken)
    {
        var result = await commentService.RejectCommentAsync(commentId, cancellationToken);
        if (!result)
        {
            return Result<bool>.Failure("رد کامنت با شکست مواجه شد");
        }

        return Result<bool>.Success(true, "عملیات با موفقیت انجام شد");
    }

    public async Task<Result<bool>> DeleteCommentAsync(int commentId, CancellationToken cancellationToken)
    {
        var result = await commentService.DeleteCommentAsync(commentId, cancellationToken);
        if (!result)
        {
            return Result<bool>.Failure("حذف کامنت با شکست مواجه شد");
        }

        return Result<bool>.Success(true, "عملیات با موفقیت انجام شد");
    }

}