using Blog.Domain.core._common;
using Blog.Domain.core.Category.Entities;
using Blog.Domain.core.Category.Service;
using Blog.Domain.core.Post.AppService;
using Blog.Domain.core.Post.DTOs;
using Blog.Domain.core.Post.Service;
using Blog.Framework;

namespace Blog.Domain.AppService;

public class PostAppService(IPostService postService) : IPostAppService
{
    public async Task<Result<bool>> CreateAsync(CreatePostDto postDto, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(postDto.Title))
            return Result<bool>.Failure("عنوان پست نمیتواند خالی باشد");

        if (string.IsNullOrWhiteSpace(postDto.Description))
            return Result<bool>.Failure("توضیحات نمیتواند خالی باشد");

        if (string.IsNullOrWhiteSpace(postDto.Slug))
            return Result<bool>.Failure("slug نمیتواند خالی باشد");

        postDto.Slug = postDto.Slug.ToSlug();

        if (postDto.AuthorId <= 0)
            return Result<bool>.Failure("ایدی نویسنده معتبر نیست");

        if (postDto.CategoryId <= 0)
            return Result<bool>.Failure("دسته بندی معتبر نیست");

        if (await postService.IsSlugExistAsync(postDto.Slug, cancellationToken))
            return Result<bool>.Failure("این slug از قبل وجود دارد امکان ثبت تکراری نیست");

        if (!string.IsNullOrWhiteSpace(postDto.Img))
        {
            var allowedExtensions = new[] { ".png", ".jpg", ".jpeg" };
            var extension = Path.GetExtension(postDto.Img)?.ToLower();

            if (string.IsNullOrEmpty(extension) || !allowedExtensions.Contains(extension))
                return Result<bool>.Failure("پسوند تصویر نامعتبر است. فقط png و jpg مجاز هستند");
        }
        else
        {
            return Result<bool>.Failure("تصویر پست نمی‌تواند خالی باشد");
        }

        var success = await postService.CreateAsync(postDto, cancellationToken);

        if (!success)
            return Result<bool>.Failure("ساخت پست با خطا مواجه شد");

        return Result<bool>.Success(true, "پست با موفقیت ساخته شد");
    }

    public async Task<Result<bool>> EditAsync(EditPostDto postDto, CancellationToken cancellationToken)
    {
        if (postDto.PostId <= 0)
            return Result<bool>.Failure("پست ایدی معتبر نیست");

        var post = await postService.GetByAsync(postDto.PostId, cancellationToken);
        if (post == null)
            return Result<bool>.Failure("پست یافت نشد");

        if (string.IsNullOrWhiteSpace(postDto.Title))
            return Result<bool>.Failure("عنوان نمیتواند خالی باشد");

        if (string.IsNullOrWhiteSpace(postDto.Description))
            return Result<bool>.Failure("توضیحات نمیتواند خالی باشد");

        if (string.IsNullOrWhiteSpace(postDto.Slug))
            return Result<bool>.Failure("slug نمیتواند خالی باشد");

        postDto.Slug = postDto.Slug.ToSlug();

        if (postDto.CategoryId <= 0)
            return Result<bool>.Failure("دسته بندی معتبر نیست");

        if (post.Slug != postDto.Slug)
        {
            if (await postService.IsSlugExistAsync(postDto.Slug, cancellationToken))
                return Result<bool>.Failure("slug تکراری است");
        }

        var success = await postService.EditAsync(postDto, cancellationToken);

        if (!success)
            return Result<bool>.Failure("ویرایش پست با خطا مواجه شد");

        return Result<bool>.Success(true, "پست با موفقیت ویرایش شد");
    }

    public async Task<Result<PostDto>> GetByAsync(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
            return Result<PostDto>.Failure("ایدی معتبر نیست");

        var post = await postService.GetByAsync(id, cancellationToken);

        if (post == null)
            return Result<PostDto>.Failure("پست یافت نشد");

        return Result<PostDto>.Success(post);
    }

    public async Task<Result<PostDto>> GetByAsync(string slug, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(slug))
            return Result<PostDto>.Failure("اسلاگ نمیتواند خالی باشد");

        var post = await postService.GetByAsync(slug, cancellationToken);

        if (post == null)
            return Result<PostDto>.Failure("پست یافت نشد");

        return Result<PostDto>.Success(post);
    }

    public async Task<PostFilterDto> GetPostsByFilterAsync(PostFilterParams filterParams,
        CancellationToken cancellationToken)
    {
        return await postService.GetPostByFilterAsync(filterParams, cancellationToken);
    }

    public async Task<List<PostDto>> GetRecentlyPostsAsync(int count, CancellationToken cancellationToken)
    {
        return await postService.GetRecentlyPostsAsync(count, cancellationToken);
    }

    public async Task<bool> IncreasePostViewsAsync(int postId, CancellationToken cancellationToken)
    {
        return await postService.IncreasePostViewsAsync(postId, cancellationToken);
    }

    public async Task<List<PostDto>> GetAllByAsync(PostSearchFilter filter, CancellationToken cancellationToken)
    {
        return await postService.GetAllByAsync(filter, cancellationToken);
    }

    public async Task<List<PostDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await postService.GetAllAsync(cancellationToken);
    }

    public async Task<Result<bool>> DeleteAsync(int postId, CancellationToken cancellationToken)
    {
        var result = await postService.DeleteAsync(postId, cancellationToken);
        if (!result)
        {
            return Result<bool>.Failure("حذف با خطا رخ داد");
        }
        return Result<bool>.Failure("حذف با موفقیت انجام شد");
    }
}