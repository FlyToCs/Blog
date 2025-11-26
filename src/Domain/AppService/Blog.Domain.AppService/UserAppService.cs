using Blog.Domain.core._common;
using Blog.Domain.core.User.AppService;
using Blog.Domain.core.User.DTOs;
using Blog.Domain.core.User.Service;
using Blog.Framework;

namespace Blog.Domain.AppService;

public class UserAppService(IUserService userService) : IUserAppService
{
    public Result<UserDto> Login(string username, string password)
    {
        var user = userService.GetByUserName(username);
        if (user == null)
        {
            return Result<UserDto>.Failure("نام کاربری یا رمز عبور اشتباه است");
        }
        else
        {
            if (!PasswordHasherSha256.VerifyPassword(password, user.PasswordHash))
            {
                return Result<UserDto>.Failure("نام کاربری یا رمز عبور اشتباه است");
            }
        }

        if (!user.IsActive)
        {
            return Result<UserDto>.Failure("حساب کاربری فعال نیست");
        }
        return Result<UserDto>.Success(new UserDto()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            ImgUrl = user.ImgUrl,
            Role = user.Role,
            IsActive = user.IsActive,
            Id = user.Id
        });
    }

    public Result<bool> Register(CreateUserDto userDto)
    {
        userService.Create(userDto);
        return Result<bool>.Success();
    }

    public Result<bool> Create(CreateUserDto userDto)
    {
        userService.Create(userDto);
        return Result<bool>.Success();
    }

    public List<UserDto> GetAll()
    {
       return userService.GetAll();
    }

    public Result<UserDto> GetById(int id)
    {
        var user = userService.GetById(id);
        if (user == null)
        {
            return Result<UserDto>.Failure("کاربری با این ایدی یافت نشد");
        }

        return Result<UserDto>.Success(user);
    }

    public Result<bool> Update(int userId, UserWithPasswordDto userDto)
    {
        userService.Update(userId, userDto);
        return Result<bool>.Success();
    }

    public Result<bool> ChangePassword(int userId, string newPassword)
    {
        newPassword = PasswordHasherSha256.HashPassword(newPassword);
        userService.ChangePassword(userId, newPassword);
        return Result<bool>.Success();
    }

    public Result<bool> Activate(int userId)
    {
        var result = userService.Activate(userId);
        if (!result)
        {
            return Result<bool>.Failure("عملیات غیر فعال سازی کاربر با شکست مواجه شد");
        }
        return Result<bool>.Success();

    }

    public Result<bool> DeActivate(int userId)
    {
        var result = userService.DeActivate(userId);
        if (!result)
        {
            return Result<bool>.Failure("عملیات فعال سازی کاربر با شکست مواجه شد");
        }
        return Result<bool>.Success();
    }
}