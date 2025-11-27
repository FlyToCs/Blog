using Blog.Domain.core._common;
using Blog.Domain.core.User.AppService;
using Blog.Domain.core.User.DTOs;
using Blog.Domain.core.User.Service;
using Blog.Framework;

namespace Blog.Domain.AppService;

public class UserAppService(IUserService userService) : IUserAppService
{
    public async Task<Result<UserDto>> LoginAsync(string username, string password)
    {
        var user = await userService.GetByUserNameAsync(username);
        if (user == null || !PasswordHasherSha256.VerifyPassword(password, user.PasswordHash))
            return Result<UserDto>.Failure("نام کاربری یا رمز عبور اشتباه است");

        if (!user.IsActive)
            return Result<UserDto>.Failure("حساب کاربری فعال نیست");

        return Result<UserDto>.Success(new UserDto()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            ImgUrl = user.ImgUrl,
            Role = user.Role,
            IsActive = user.IsActive
        });
    }

    public async Task<Result<bool>> RegisterAsync(CreateUserDto userDto)
    {
        var success = await userService.CreateAsync(userDto);
        if (!success)
            return Result<bool>.Failure("ثبت نام با خطا مواجه شد");

        return Result<bool>.Success(true, "ثبت نام با موفقیت انجام شد");
    }

    public async Task<Result<bool>> CreateAsync(CreateUserDto userDto)
    {
        var success = await userService.CreateAsync(userDto);
        if (!success)
            return Result<bool>.Failure("ایجاد کاربر با خطا مواجه شد");

        return Result<bool>.Success(true, "کاربر با موفقیت ساخته شد");
    }

    public async Task<List<UserDto>> GetAllAsync()
    {
        return await userService.GetAllAsync();
    }

    public async Task<Result<UserDto>> GetByIdAsync(int id)
    {
        var user = await userService.GetByIdAsync(id);
        if (user == null)
            return Result<UserDto>.Failure("کاربری با این ایدی یافت نشد");

        return Result<UserDto>.Success(user);
    }

    public async Task<Result<bool>> UpdateAsync(int userId, UserWithPasswordDto userDto)
    {
        var success = await userService.UpdateAsync(userId, userDto);
        if (!success)
            return Result<bool>.Failure("ویرایش کاربر با شکست مواجه شد");

        return Result<bool>.Success(true, "ویرایش با موفقیت انجام شد");
    }

    public async Task<Result<bool>> ChangePasswordAsync(int userId, string newPassword)
    {
        newPassword = PasswordHasherSha256.HashPassword(newPassword);
        var success = await userService.ChangePasswordAsync(userId, newPassword);
        if (!success)
            return Result<bool>.Failure("تغییر رمز عبور با شکست مواجه شد");

        return Result<bool>.Success(true, "رمز عبور با موفقیت تغییر کرد");
    }

    public async Task<Result<bool>> ActivateAsync(int userId)
    {
        var success = await userService.ActivateAsync(userId);
        if (!success)
            return Result<bool>.Failure("عملیات فعال سازی کاربر با شکست مواجه شد");

        return Result<bool>.Success(true, "کاربر با موفقیت فعال شد");
    }

    public async Task<Result<bool>> DeActivateAsync(int userId)
    {
        var success = await userService.DeActivateAsync(userId);
        if (!success)
            return Result<bool>.Failure("عملیات غیر فعال سازی کاربر با شکست مواجه شد");

        return Result<bool>.Success(true, "کاربر با موفقیت غیر فعال شد");
    }

}