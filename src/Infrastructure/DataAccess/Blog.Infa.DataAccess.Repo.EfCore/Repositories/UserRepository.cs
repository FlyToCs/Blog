using Blog.Domain.core.User.Data;
using Blog.Domain.core.User.DTOs;
using Blog.Domain.core.User.Entities;
using Blog.Domain.core.User.Enums;
using Blog.Infa.Db.SqlServer.EfCore.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infa.DataAccess.Repo.EfCore.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task<bool> CreateAsync(CreateUserDto userDto, CancellationToken cancellationToken)
    {
        var user = new User()
        {
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            UserName = userDto.UserName,
            PasswordHash = userDto.Password,
            ImgUrl = userDto.ImgUrl!,
            Role = RoleEnum.User,
            IsActive = true,
            IsDeleted = false
        };

        await context.AddAsync(user);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<List<UserDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context.Users
            .Select(u => new UserDto()
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                UserName = u.UserName,
                ImgUrl = u.ImgUrl,
                Role = u.Role,
                IsActive = u.IsActive
            })
            .ToListAsync();
    }

    public async Task<UserDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null) return null;

        return new UserDto()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            ImgUrl = user.ImgUrl,
            Role = user.Role,
            IsActive = user.IsActive
        };
    }

    public async Task<UserWithPasswordDto?> GetByUserNameAsync(string username, CancellationToken cancellationToken)
    {
        return await context.Users
            .Where(u => u.UserName == username)
            .Select(u => new UserWithPasswordDto()
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                UserName = u.UserName,
                PasswordHash = u.PasswordHash,
                ImgUrl = u.ImgUrl,
                Role = u.Role,
                IsActive = u.IsActive
            })
            .FirstOrDefaultAsync();
    }

    public async Task<bool> UpdateAsync(int userId, UserWithPasswordDto userDto, CancellationToken cancellationToken)
    {
        var user = await context.Users.FindAsync(userId);
        if (user == null) return false;

        user.FirstName = userDto.FirstName;
        user.LastName = userDto.LastName;
        user.UserName = userDto.UserName;
        user.PasswordHash = userDto.PasswordHash;
        user.ImgUrl = userDto.ImgUrl;
        user.Role = userDto.Role;
        user.IsActive = userDto.IsActive;

        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> ChangePasswordAsync(int userId, string newPassword, CancellationToken cancellationToken)
    {
        var user = await context.Users.FindAsync(userId);
        if (user == null) return false;

        user.PasswordHash = newPassword;
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> ActivateAsync(int userId, CancellationToken cancellationToken)
    {
        var user = await context.Users.FindAsync(userId);
        if (user == null) return false;

        user.IsActive = true;
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeActivateAsync(int userId, CancellationToken cancellationToken)
    {
        var user = await context.Users.FindAsync(userId);
        if (user == null) return false;

        user.IsActive = false;
        return await context.SaveChangesAsync() > 0;
    }
}