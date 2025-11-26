namespace Blog.Domain.core.PostComment.DTOs;

public class CommentDto
{
    public string Text { get; set; }
    public int PostId { get; set; }
    public int UserId { get; set; }
    public string FullName { get; set; }
    public DateTime CreatedAt { get; set; }

}