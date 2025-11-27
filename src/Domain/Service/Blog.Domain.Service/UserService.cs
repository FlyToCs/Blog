using Blog.Domain.core.User.Data;
using Blog.Domain.core.User.DTOs;
using Blog.Domain.core.User.Service;

namespace Blog.Domain.Service;

public class UserService(IUserRepository userRepo) : IUserService
{

    public async Task<bool> CreateAsync(CreateUserDto userDto)
    {
        return await userRepo.CreateAsync(userDto);
    }

    public async Task<List<UserDto>> GetAllAsync()
    {
        return await userRepo.GetAllAsync();
    }

    public async Task<UserDto?> GetByIdAsync(int id)
    {
        return await userRepo.GetByIdAsync(id);
    }

    public async Task<UserWithPasswordDto?> GetByUserNameAsync(string username)
    {
        return await userRepo.GetByUserNameAsync(username);
    }

    public async Task<bool> UpdateAsync(int userId, UserWithPasswordDto userDto)
    {
        return await userRepo.UpdateAsync(userId, userDto);
    }

    public async Task<bool> ChangePasswordAsync(int userId, string newPassword)
    {
        return await userRepo.ChangePasswordAsync(userId, newPassword);
    }

    public async Task<bool> ActivateAsync(int userId)
    {
        return await userRepo.ActivateAsync(userId);
    }

    public async Task<bool> DeActivateAsync(int userId)
    {
        return await userRepo.DeActivateAsync(userId);
    }
}