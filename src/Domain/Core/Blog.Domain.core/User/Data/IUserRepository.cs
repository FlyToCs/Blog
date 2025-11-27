using Blog.Domain.core.User.DTOs;

namespace Blog.Domain.core.User.Data;

public interface IUserRepository
{
    Task<bool> CreateAsync(CreateUserDto userDto);
    Task<List<UserDto>> GetAllAsync();
    Task<UserDto?> GetByIdAsync(int id);
    Task<UserWithPasswordDto?> GetByUserNameAsync(string username);
    Task<bool> UpdateAsync(int userId, UserWithPasswordDto userDto);
    Task<bool> ChangePasswordAsync(int userId, string newPassword);
    Task<bool> ActivateAsync(int userId);
    Task<bool> DeActivateAsync(int userId);

}