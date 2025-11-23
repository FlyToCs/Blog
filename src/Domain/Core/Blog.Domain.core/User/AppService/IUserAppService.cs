using Blog.Domain.core._common;
using Blog.Domain.core.User.DTOs;

namespace Blog.Domain.core.User.AppService;

public interface IUserAppService
{
    Result<UserDto> Login(string username, string password);
    Result<bool> Register(CreateUserDto userDto);
    Result<bool> Create(CreateUserDto userDto);
    List<UserDto> GetAll();
    Result<UserDto> GetById(int id); 
    Result<bool> Update(int userId, UserWithPasswordDto userDto);
    Result<bool> ChangePassword(int userId, string newPassword);
    Result<bool> Activate(int userId);
    Result<bool> DeActivate(int userId);
}