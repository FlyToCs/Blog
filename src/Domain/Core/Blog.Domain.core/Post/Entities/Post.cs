using System.Security.Cryptography.X509Certificates;
using Blog.Domain.core._common;

namespace Blog.Domain.core.Post.Entities;

public class Post : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Context { get; set; }
    public int visits { get; set; }
    public string Slug { get; set; }
    public string Img { get; set; }

    public int AuthorId { get; set; }
    public User.Entities.User Author { get; set; }
    public int CategoryId { get; set; }
    public int? SubCategoryId { get; set; }
    
    public Category.Entities.Category Category { get; set; }
    public Category.Entities.Category SubCategory { get; set; }
    public List<PostComment.Entities.PostComment> PostComments { get; set; }


}