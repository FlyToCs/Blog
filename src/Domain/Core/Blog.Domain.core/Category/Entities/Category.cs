using Blog.Domain.core._common;

namespace Blog.Domain.core.Category.Entities;

public class Category : BaseEntity
{
    public string Title { get; set; }
    public string Slug { get; set; }
    public string MetaTag { get; set; }
    public string MetaDescription { get; set; }

    public int? ParentId { get; set; }

    public Category Parent { get; set; }
    public List<Category> Children { get; set; } = new();

    public List<Post.Entities.Post> Posts { get; set; }
    public int UserId { get; set; }
    public User.Entities.User User { get; set; }
}