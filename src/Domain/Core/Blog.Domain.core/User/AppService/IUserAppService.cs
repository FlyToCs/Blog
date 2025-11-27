using Blog.Domain.core._common;
using Blog.Domain.core.User.DTOs;

namespace Blog.Domain.core.User.AppService;

public interface IUserAppService
{
    Task<Result<UserDto?>> LoginAsync(string username, string password);
    Task<Result<bool>> RegisterAsync(CreateUserDto userDto);
    Task<Result<bool>> CreateAsync(CreateUserDto userDto);
    Task<List<UserDto>> GetAllAsync();
    Task<Result<UserDto?>> GetByIdAsync(int id);
    Task<Result<bool>> UpdateAsync(int userId, UserWithPasswordDto userDto);
    Task<Result<bool>> ChangePasswordAsync(int userId, string newPassword);
    Task<Result<bool>> ActivateAsync(int userId);
    Task<Result<bool>> DeActivateAsync(int userId);
}