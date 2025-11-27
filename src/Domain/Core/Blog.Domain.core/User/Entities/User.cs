using System.Security.AccessControl;
using Blog.Domain.core._common;
using Blog.Domain.core.User.Enums;

namespace Blog.Domain.core.User.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string PasswordHash { get; set; }
    public string ImgUrl { get; set; }
    public RoleEnum Role { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }

    public List<PostComment.Entities.PostComment> PostComments { get; set; }
    public List<Post.Entities.Post> Posts { get; set; }
    public List<Category.Entities.Category> Categories { get; set; }
}