using Blog.Domain.core.PostComment.Data;
using Blog.Domain.core.PostComment.DTOs;
using Blog.Domain.core.PostComment.Entities;
using Blog.Infa.Db.SqlServer.EfCore.DbContexts;

namespace Blog.Infa.DataAccess.Repo.EfCore.Repositories;

public class CommentRepository(AppDbContext context) : ICommentRepository 
{
    public bool CreateComment(CreateCommentDto commentDto)
    {
        var comment = new PostComment()
        {
            Text = commentDto.Text,
            UserId = commentDto.UserId,
            PostId = commentDto.PostId
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
                CreatedAt = c.CreatedAt,
                FullName = c.User.FirstName +" " +c.User.LastName,
                UserId = c.UserId
            }).ToList();
    }
}