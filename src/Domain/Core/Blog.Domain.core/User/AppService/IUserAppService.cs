using Blog.Domain.core._common;
using Blog.Domain.core.User.DTOs;

namespace Blog.Domain.core.User.AppService;

public interface IUserAppService
{
    Task<Result<UserDto>> LoginAsync(string username, string password, CancellationToken cancellationToken);
    Task<Result<bool>> RegisterAsync(CreateUserDto userDto, CancellationToken cancellationToken);
    Task<Result<bool>> CreateAsync(CreateUserDto userDto, CancellationToken cancellationToken);
    Task<List<UserDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<Result<UserDto>> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<Result<bool>> UpdateAsync(int userId, UserWithPasswordDto userDto, CancellationToken cancellationToken);
    Task<Result<bool>> ChangePasswordAsync(int userId, string newPassword, CancellationToken cancellationToken);
    Task<Result<bool>> ActivateAsync(int userId, CancellationToken cancellationToken);
    Task<Result<bool>> DeActivateAsync(int userId, CancellationToken cancellationToken);
}