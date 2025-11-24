using Blog.Domain.core.Category.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infa.Db.SqlServer.EfCore.Configs;

public class CategoryConfigs : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        builder.HasKey(x => x.Id);

        builder.Property(c => c.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.Slug)
            .IsRequired()
            .HasMaxLength(200);
        builder.HasIndex(c => c.Slug).IsUnique();

        builder.Property(c => c.MetaTag)
            .HasMaxLength(150);

        builder.Property(c => c.MetaDesctiption)
            .HasMaxLength(300);

        builder.Property(u => u.CreatedAt)
            .HasDefaultValueSql("GetDate()")
            .ValueGeneratedOnAdd();

        builder.HasMany(c => c.Posts)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(c => c.Parent)
            .WithMany(c => c.Children)
            .HasForeignKey(c => c.ParentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}