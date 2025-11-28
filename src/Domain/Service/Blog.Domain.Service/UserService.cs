using Blog.Domain.core.User.Data;
using Blog.Domain.core.User.DTOs;
using Blog.Domain.core.User.Service;

namespace Blog.Domain.Service;

public class UserService(IUserRepository userRepo) : IUserService
{

    public async Task<bool> CreateAsync(CreateUserDto userDto, CancellationToken cancellationToken)
    {
        return await userRepo.CreateAsync(userDto, cancellationToken);
    }

    public async Task<List<UserDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await userRepo.GetAllAsync(cancellationToken);
    }

    public async Task<UserDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await userRepo.GetByIdAsync(id, cancellationToken);
    }

    public async Task<UserWithPasswordDto?> GetByUserNameAsync(string username, CancellationToken cancellationToken)
    {
        return await userRepo.GetByUserNameAsync(username, cancellationToken);
    }

    public async Task<bool> UpdateAsync(int userId, UserWithPasswordDto userDto, CancellationToken cancellationToken)
    {
        return await userRepo.UpdateAsync(userId, userDto, cancellationToken);
    }

    public async Task<bool> ChangePasswordAsync(int userId, string newPassword, CancellationToken cancellationToken)
    {
        return await userRepo.ChangePasswordAsync(userId, newPassword, cancellationToken);
    }

    public async Task<bool> ActivateAsync(int userId, CancellationToken cancellationToken)
    {
        return await userRepo.ActivateAsync(userId, cancellationToken);
    }

    public async Task<bool> DeActivateAsync(int userId, CancellationToken cancellationToken)
    {
        return await userRepo.DeActivateAsync(userId, cancellationToken);
    }
}