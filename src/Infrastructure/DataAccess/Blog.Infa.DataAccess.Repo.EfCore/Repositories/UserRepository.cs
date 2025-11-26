using Blog.Domain.core.User.Data;
using Blog.Domain.core.User.DTOs;
using Blog.Domain.core.User.Entities;
using Blog.Domain.core.User.Enums;
using Blog.Infa.Db.SqlServer.EfCore.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infa.DataAccess.Repo.EfCore.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public bool Create(CreateUserDto userDto)
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
        context.Add(user);
        return context.SaveChanges() > 0;
    }

    public List<UserDto> GetAll()
    {
        return context.Users.Select(u => new UserDto()
        {
            Id = u.Id,
            FirstName = u.FirstName,
            LastName = u.LastName,
            UserName = u.UserName,
            ImgUrl = u.ImgUrl,
            Role = u.Role,
            IsActive = u.IsActive
        }).ToList();
    }

    public UserDto? GetById(int id)
    {
        var user = context.Users.Find(id);
        return new UserDto()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            ImgUrl = user.ImgUrl,
            Role = user.Role,
            IsActive = user.IsActive
        };
    }

    public UserWithPasswordDto? GetByUserName(string username)
    {
        return context.Users.Where(u => u.UserName == username).Select(u => new UserWithPasswordDto()
        {
            Id = u.Id,
            FirstName = u.FirstName,
            LastName = u.LastName,
            UserName = u.UserName,
            PasswordHash = u.PasswordHash,
            ImgUrl = u.ImgUrl,
            Role = u.Role,
            IsActive = u.IsActive
        }).FirstOrDefault();
    }

    public bool Update(int userId, UserWithPasswordDto userDto)
    {
        var effectedRows = context.Users.Where(u => u.Id == userId)
            .ExecuteUpdate(setter => setter
                .SetProperty(u => u.FirstName, userDto.FirstName)
                .SetProperty(u => u.LastName, userDto.LastName)
                .SetProperty(u => u.UserName, userDto.UserName)
                .SetProperty(u => u.PasswordHash, userDto.PasswordHash)
                .SetProperty(u => u.ImgUrl, userDto.ImgUrl)
                .SetProperty(u => u.Role, userDto.Role)
                .SetProperty(u => u.IsActive, userDto.IsActive));
        return effectedRows > 0;
    }

    public bool ChangePassword(int userId, string newPassword)
    {
        var effectedRows = context.Users.Where(u => u.Id == userId)
            .ExecuteUpdate(setter => setter
                .SetProperty(u => u.PasswordHash, newPassword));
        return effectedRows > 0;
    }

    public bool Activate(int userId)
    {
        var effectedRows = context.Users.Where(u => u.Id == userId)
            .ExecuteUpdate(setter => setter
                .SetProperty(u => u.IsActive, true));
        return effectedRows > 0;
    }

    public bool DeActivate(int userId)
    {
        var effectedRows = context.Users.Where(u => u.Id == userId)
            .ExecuteUpdate(setter => setter
                .SetProperty(u => u.IsActive, false));
        return effectedRows > 0;
    }
}