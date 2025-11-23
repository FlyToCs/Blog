using Blog.Domain.core.User.DTOs;

namespace Blog.Domain.core.User.Service;

public interface IUserService
{
    bool Create(CreateUserDto userDto);
    List<UserDto> GetAll();
    UserDto? GetById(int id);
    UserWithPasswordDto? GetByUserName(string username);
    bool Update(int userId, UserWithPasswordDto userDto);
    bool ChangePassword(int userId, string newPassword);
    bool Activate(int userId);
    bool DeActivate(int userId);
}