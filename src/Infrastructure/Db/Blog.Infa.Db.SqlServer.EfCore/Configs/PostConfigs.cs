using Blog.Domain.core.Post.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infa.Db.SqlServer.EfCore.Configs;

public class PostConfigs : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasQueryFilter(c => !c.IsDeleted);

        builder.ToTable("Posts");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Description)
            .IsRequired();

        builder.Property(p => p.visits)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(p => p.Slug)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Img).HasMaxLength(100);

        builder.Property(u => u.CreatedAt)
            .HasDefaultValueSql("GetDate()")
            .ValueGeneratedOnAdd();

        builder.HasOne(p => p.Author)
            .WithMany(u=>u.Posts)
            .HasForeignKey(p => p.AuthorId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(p => p.Category)
            .WithMany(c => c.Posts)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(p => p.SubCategory)
            .WithMany()
            .HasForeignKey(p => p.SubCategoryId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);


        builder.HasMany(p => p.PostComments)
            .WithOne(pc => pc.Post)
            .HasForeignKey(pc => pc.PostId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}