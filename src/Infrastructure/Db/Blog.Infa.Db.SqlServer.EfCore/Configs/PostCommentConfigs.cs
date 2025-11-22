using Blog.Domain.core.PostComment.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infa.Db.SqlServer.EfCore.Configs;

public class PostCommentConfigs : IEntityTypeConfiguration<PostComment>
{
    public void Configure(EntityTypeBuilder<PostComment> builder)
    {
        builder.ToTable("PostComments");

        builder.HasKey(pc => pc.Id);

        builder.Property(u => u.CreatedAt)
            .HasDefaultValueSql("GetDate()")
            .ValueGeneratedOnAdd();

        builder.Property(pc => pc.Text)
            .IsRequired()
            .HasMaxLength(1000);

        builder.HasOne(pc => pc.Post)
            .WithMany(p => p.PostComments)
            .HasForeignKey(pc => pc.PostId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(pc => pc.User)
            .WithMany(u => u.PostComments)
            .HasForeignKey(pc => pc.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}