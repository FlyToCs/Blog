using System.ComponentModel;
using Blog.Domain.core.Category.DTOs;

namespace Blog.Domain.core.Post.DTOs;

public class PostDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Slug { get; set; }
    public int PostViews { get; set; }
    public string AuthorName { get; set; }
    public string Context { get; set; }
    public int AuthorId { get; set; }
    public int PostId { get; set; }
    public string Img { get; set; }
    public DateTime CreatedAt { get; set; }
    public int CategoryId { get; set; }
    public string CategoryTitle { get; set; }
    public string CategorySlug { get; set; }

    public int? SubCategoryId { get; set; }
    public string? SubCategoryTitle { get; set; }
    public string? SubCategorySlug { get; set; }

}