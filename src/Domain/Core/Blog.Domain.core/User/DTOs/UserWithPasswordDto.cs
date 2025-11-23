using Blog.Domain.core.User.Enums;

namespace Blog.Domain.core.User.DTOs;

public class UserWithPasswordDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string PasswordHash { get; set; }
    public string ImgUrl { get; set; }
    public RoleEnum Role { get; set; }
    public bool IsActive { get; set; }
}