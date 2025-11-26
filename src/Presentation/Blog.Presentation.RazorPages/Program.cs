using Blog.Domain.AppService;
using Blog.Domain.core.Category.AppService;
using Blog.Domain.core.Category.Data;
using Blog.Domain.core.Category.Service;
using Blog.Domain.core.Post.AppService;
using Blog.Domain.core.Post.Data;
using Blog.Domain.core.Post.Service;
using Blog.Domain.core.PostComment.AppService;
using Blog.Domain.core.PostComment.Data;
using Blog.Domain.core.PostComment.Service;
using Blog.Domain.core.User.AppService;
using Blog.Domain.core.User.Data;
using Blog.Domain.core.User.Service;
using Blog.Domain.Service;
using Blog.Infa.DataAccess.Repo.EfCore.Repositories;
using Blog.Infa.Db.SqlServer.EfCore.DbContexts;
using Blog.Presentation.RazorPages.Services.FileManager;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.AccessDeniedPath = "/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromDays(30);
    });
builder.Services.AddScoped<CookieManagementService>();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserAppService, UserAppService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryAppService, CategoryAppService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();


builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IPostAppService, PostAppService>();
builder.Services.AddScoped<IPostRepository, PostRepository>();


builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ICommentAppService, CommentAppService>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

builder.Services.AddScoped<IFileManager, FileManager>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
