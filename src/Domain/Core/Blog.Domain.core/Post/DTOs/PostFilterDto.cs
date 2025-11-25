namespace Blog.Domain.core.Post.DTOs;

public class PostFilterDto
{

    public PostFilterParams FilterParams { get; set; }
    public List<PostDto> Posts { get; set; }
    public int PageCount { get; set; }

}

public class PostFilterParams
{
    public int PageId { get; set; }
    public int Take { get; set; }
    public string Title { get; set; }
    public string CategorySlug { get; set; }
}