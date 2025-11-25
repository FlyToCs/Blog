namespace Blog.Domain.core.Post.DTOs;

public class EditPostDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Slug { get; set; }
    public string Context { get; set; }

    public int CategoryId { get; set; }
    public int PostId { get; set; }
    public int? SubCategoryId { get; set; }
    public string Img { get; set; }
}