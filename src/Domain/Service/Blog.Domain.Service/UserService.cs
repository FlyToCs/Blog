using Blog.Domain.core.User.Data;
using Blog.Domain.core.User.DTOs;
using Blog.Domain.core.User.Service;

namespace Blog.Domain.Service;

public class UserService(IUserRepository userRepo) : IUserService
{
    public bool Create(CreateUserDto userDto)
    {
        return userRepo.Create(userDto);
    }

    public List<UserDto> GetAll()
    {
        return userRepo.GetAll();
    }

    public UserDto? GetById(int id)
    {
        return userRepo.GetById(id);
    }

    public UserWithPasswordDto? GetByUserName(string username)
    {
        return userRepo.GetByUserName(username);
    }

    public bool Update(int userId, UserWithPasswordDto userDto)
    {
        return userRepo.Update(userId, userDto);
    }

    public bool ChangePassword(int userId, string newPassword)
    {
        return userRepo.ChangePassword(userId, newPassword);
    }

    public bool Activate(int userId)
    {
        return userRepo.Activate(userId);
    }

    public bool DeActivate(int userId)
    {
        return userRepo.DeActivate(userId);
    }
}