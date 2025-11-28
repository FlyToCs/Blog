using Blog.Domain.core.PostComment.Data;
using Blog.Domain.core.PostComment.DTOs;
using Blog.Domain.core.PostComment.Entities;
using Blog.Domain.core.PostComment.Enums;
using Blog.Infa.Db.SqlServer.EfCore.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infa.DataAccess.Repo.EfCore.Repositories;

public class CommentRepository(AppDbContext context) : ICommentRepository 
{
    public async Task<bool> CreateCommentAsync(CreateCommentDto commentDto, CancellationToken cancellationToken)
    {
        var comment = new PostComment()
        {
            Text = commentDto.Text,
            UserId = commentDto.UserId,
            PostId = commentDto.PostId,
            Rate = commentDto.Rate,
            Status = CommentStatus.Pending
        };

        await context.AddAsync(comment,cancellationToken);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<List<CommentDto>> GetCommentsPostAsync(int userId, CancellationToken cancellationToken)
    {
        return await context.PostComments
            .Where(c => c.Post.AuthorId == userId)
            .Select(c => new CommentDto()
            {
                Id = c.Id,
                PostId = c.PostId,
                Text = c.Text,
                Status = c.Status,
                Rate = c.Rate,
                CreatedAt = c.CreatedAt,
                FullName = c.User.FirstName + " " + c.User.LastName,
                UserId = c.UserId
            })
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<bool> ApproveCommentAsync(int commentId, CancellationToken cancellationToken)
    {
        var comment = await context.PostComments.FirstOrDefaultAsync(c=>c.Id == commentId, cancellationToken);
        if (comment == null) return false;

        comment.Status = CommentStatus.Approved;
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> RejectCommentAsync(int commentId, CancellationToken cancellationToken)
    {
        var comment = await context.PostComments.FirstOrDefaultAsync(c=>c.Id == commentId, cancellationToken);
        if (comment == null) return false;

        comment.Status = CommentStatus.Rejected;
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteCommentAsync(int commentId, CancellationToken cancellationToken)
    {
        var comment = await context.PostComments.FirstOrDefaultAsync(c=>c.Id == commentId, cancellationToken);
        if (comment == null) return false;

        comment.IsDeleted = true;
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }
}