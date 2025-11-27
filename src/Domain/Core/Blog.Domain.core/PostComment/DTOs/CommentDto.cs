using Blog.Domain.core.PostComment.Enums;

namespace Blog.Domain.core.PostComment.DTOs;

public class CommentDto
{

    public int Id { get; set; }
    public string Text { get; set; }
    public int PostId { get; set; }
    public int UserId { get; set; }
    public string FullName { get; set; }
    public DateTime CreatedAt { get; set; }
    public CommentStatus Status { get; set; }
    public RateEnum Rate { get; set; }

}