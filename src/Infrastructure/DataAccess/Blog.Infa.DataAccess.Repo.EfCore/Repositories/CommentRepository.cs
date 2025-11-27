using Blog.Domain.core.PostComment.Data;
using Blog.Domain.core.PostComment.DTOs;
using Blog.Domain.core.PostComment.Entities;
using Blog.Domain.core.PostComment.Enums;
using Blog.Infa.Db.SqlServer.EfCore.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infa.DataAccess.Repo.EfCore.Repositories;

public class CommentRepository(AppDbContext context) : ICommentRepository 
{
    public bool CreateComment(CreateCommentDto commentDto)
    {
        var comment = new PostComment()
        {
            Text = commentDto.Text,
            UserId = commentDto.UserId,
            PostId = commentDto.PostId,
            Rate = commentDto.Rate,
            Status = CommentStatus.Pending
        };
        context.Add(comment);
        return context.SaveChanges() > 0;
    }

    public List<CommentDto> GetCommentsPost(int postId)
    {
        return context.PostComments.Where(c => c.PostId == postId)
            .Select(c => new CommentDto()
            {
                PostId = c.PostId,
                Text = c.Text,
                Status = c.Status,
                Rate = c.Rate,
                CreatedAt = c.CreatedAt,
                FullName = c.User.FirstName +" " +c.User.LastName,
                UserId = c.UserId
            }).ToList();
    }

    public bool ApproveComment(int commentId)
    {
       var effectedRows =  context.PostComments.Where(c => c.Id == commentId)
            .ExecuteUpdate(setter => setter
                .SetProperty(c => c.Status, CommentStatus.Approved));

       return effectedRows > 0;
    }

    public bool RejectComment(int commentId)
    {
        var effectedRows = context.PostComments.Where(c => c.Id == commentId)
            .ExecuteUpdate(setter => setter
                .SetProperty(c => c.Status, CommentStatus.Rejected));

        return effectedRows > 0;
    }

    public bool DeleteComment(int commentId)
    {
        var effectedRows = context.PostComments.Where(c => c.Id == commentId)
            .ExecuteUpdate(setter => setter
                .SetProperty(c => c.IsDeleted, true));

        return effectedRows > 0;
    }
}