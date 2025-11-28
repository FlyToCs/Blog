using Blog.Domain.core._common;
using Blog.Domain.core.Category.Entities;
using Blog.Domain.core.Post.Entities;
using Blog.Domain.core.PostComment.Entities;
using Blog.Domain.core.User.Entities;
using Blog.Infa.Db.SqlServer.EfCore.Configs;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infa.Db.SqlServer.EfCore.DbContexts;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<PostComment> PostComments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfigs).Assembly);

        base.OnModelCreating(modelBuilder);
    }


    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateTimestamps()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is BaseEntity &&
                        (e.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            ((BaseEntity)entry.Entity).UpdatedAt = DateTime.Now;
        }
    }
}