using Blog.Domain.core._common;

namespace Blog.Domain.core.Post.Entities;

public class Post : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int visits { get; set; }
    public string Slug { get; set; }

    public int UserId { get; set; }
    public User.Entities.User User { get; set; }
    public Category.Entities.Category Categories { get; set; }
    public List<PostComment.Entities.PostComment> PostComments { get; set; }


}