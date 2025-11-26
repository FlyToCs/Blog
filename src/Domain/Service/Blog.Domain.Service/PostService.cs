using Blog.Domain.core.Post.Data;
using Blog.Domain.core.Post.DTOs;
using Blog.Domain.core.Post.Service;

namespace Blog.Domain.Service;

public class PostService(IPostRepository postRepo) : IPostService
{
    public bool Create(CreatePostDto postDto)
    {
        return postRepo.Create(postDto);
    }

    public bool Edit(EditPostDto postDto)
    {
        return postRepo.Edit(postDto);
    }

    public PostDto? GetBy(string slug)
    {
        return postRepo.GetBy(slug);
    }

    public PostDto? GetBy(int id)
    {
        return postRepo.GetBy(id);
    }

    public bool IsSlugExist(string slug)
    {
        return postRepo.IsSlugExist(slug);
    }

    public PostFilterDto GetPostByFilter(PostFilterParams filterParams)
    {
        return postRepo.GetPostByFilter(filterParams);
    }

    public List<PostDto> GetRecentlyPosts(int count)
    {
        return postRepo.GetRecentlyPosts(count);
    }

    public bool IncreasePostViews(int postId)
    {
        return postRepo.IncreasePostViews(postId);
    }

    public List<PostDto> GetAllBy(int userId)
    {
        return postRepo.GetAllBy(userId);
    }

    public List<PostDto> GetAll()
    {
        return postRepo.GetAll();
    }
}