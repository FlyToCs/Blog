using Blog.Domain.core.User.DTOs;

namespace Blog.Domain.core.User.Service;

public interface IUserService
{
    Task<bool> CreateAsync(CreateUserDto userDto, CancellationToken cancellationToken);
    Task<List<UserDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<UserDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<UserWithPasswordDto?> GetByUserNameAsync(string username, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(int userId, UserWithPasswordDto userDto, CancellationToken cancellationToken);
    Task<bool> ChangePasswordAsync(int userId, string newPassword, CancellationToken cancellationToken);
    Task<bool> ActivateAsync(int userId, CancellationToken cancellationToken);
    Task<bool> DeActivateAsync(int userId, CancellationToken cancellationToken);
}