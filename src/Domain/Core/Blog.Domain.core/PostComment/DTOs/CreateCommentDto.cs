using Blog.Domain.core.PostComment.Enums;

namespace Blog.Domain.core.PostComment.DTOs;

public class CreateCommentDto
{
    public string Text { get; set; }
    public int PostId { get; set; }
    public int UserId { get; set; }
    public RateEnum Rate { get; set; }
}