using Blog.Domain.core._common;

namespace Blog.Domain.core.PostComment.Entities;

public class PostComment : BaseEntity
{
    public string Text { get; set; }

    public int PostId { get; set; }
    public Post.Entities.Post Post { get; set; }
    public int UserId { get; set; }
    public User.Entities.User User { get; set; }
}