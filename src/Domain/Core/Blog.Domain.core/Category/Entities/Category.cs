using Blog.Domain.core._common;

namespace Blog.Domain.core.Category.Entities;

public class Category : BaseEntity
{
    public string Title { get; set; }
    public string Slug { get; set; }
    public string MetaTag { get; set; }
    public string MetaDesctiption { get; set; }

    public List<Post.Entities.Post> Posts { get; set; }
}