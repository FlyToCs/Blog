namespace Blog.Domain.core.Post.DTOs;

public class PostSearchFilter
{
    public int UserId { get; set; }
    public string? Title { get; set; }
    public string? CategoryName { get; set; }
}