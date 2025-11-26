using Blog.Domain.core._common;
using Blog.Domain.core.Post.AppService;
using Blog.Domain.core.Post.DTOs;
using Blog.Domain.core.Post.Service;
using Blog.Framework;

namespace Blog.Domain.AppService;

public class PostAppService(IPostService postService) : IPostAppService
{
    public Result<bool> Create(CreatePostDto postDto)
    {
        if (string.IsNullOrWhiteSpace(postDto.Title))
            return Result<bool>.Failure("عنوان پست نمیتواند خالی باشد");

        if (string.IsNullOrWhiteSpace(postDto.Description))
            return Result<bool>.Failure("توضیحات نمیتواند خالی باشد");

        if (string.IsNullOrWhiteSpace(postDto.Slug))
            return Result<bool>.Failure("slug نمیتواند خالی باشد");
        postDto.Slug.ToSlug();

        if (postDto.AuthorId <= 0)
            return Result<bool>.Failure("ایدی نویسنده معتبر نیست");

        if (postDto.CategoryId <= 0)
            return Result<bool>.Failure("دسته بندی معتبر نیست");

        if (postService.IsSlugExist(postDto.Slug))
            return Result<bool>.Failure("این slug از قبل وجود دارد امکان ثبت تکراری نیست");

        var success = postService.Create(postDto);

        if (!success)
            return Result<bool>.Failure("ساخت پست با خطا مواجه شد");

        return Result<bool>.Success(true, "پست با موقفیت ساخته شد");
    }

    public Result<bool> Edit(EditPostDto postDto)
    {
        var post = postService.GetBy(postDto.PostId);
        if (postDto.PostId <= 0)
            return Result<bool>.Failure("پست ایدی معتبر نیست");

        if (string.IsNullOrWhiteSpace(postDto.Title))
            return Result<bool>.Failure("عنوان نمیتواند خالی باشد");

        if (string.IsNullOrWhiteSpace(postDto.Description))
            return Result<bool>.Failure("توضیحات نمیتواند خالی باشد");

        if (string.IsNullOrWhiteSpace(postDto.Slug))
            return Result<bool>.Failure("slug نمیتواند خالی باشد");
        postDto.Slug.ToSlug();

        if (postDto.CategoryId <= 0)
            return Result<bool>.Failure("Invalid CategoryId.");

        if (post.Slug != postDto.Slug)
        {
            if (postService.IsSlugExist(postDto.Slug))
                return Result<bool>.Failure("slug تکراری است");
        }

        var success = postService.Edit(postDto);

        if (!success)
            return Result<bool>.Failure("ساخت پست با خطا مواجه شد");

        return Result<bool>.Success(true, "پست با موفقیت ساخته شد");
    }


    public Result<PostDto> GetBy(int id)
    {
        if (id <= 0)
            return Result<PostDto>.Failure("ایدی معتبر نیست");

        var post = postService.GetBy(id);

        if (post is null)
            return Result<PostDto>.Failure("پستی یافت نشد");

        return Result<PostDto>.Success(post);
    }

    public Result<PostDto> GetBy(string slug)
    {
        var post = postService.GetBy(slug);
        if (post == null)
        {
            return Result<PostDto>.Failure("پست یافت نشد");
        }

        return Result<PostDto>.Success(post);
    }

    public PostFilterDto GetPostsByFilter(PostFilterParams filterParams)
    {
        return postService.GetPostByFilter(filterParams);
    }

    public List<PostDto> GetRecentlyPosts(int count)
    {
        return postService.GetRecentlyPosts(count);
    }

    public bool IncreasePostViews(int postId)
    {
        return postService.IncreasePostViews(postId);
    }
}