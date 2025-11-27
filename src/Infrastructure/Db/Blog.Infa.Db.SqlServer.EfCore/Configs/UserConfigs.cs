using Blog.Domain.core.User.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infa.Db.SqlServer.EfCore.Configs;

public class UserConfigs : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {

        builder.ToTable("Users");

        builder.HasKey(x => x.Id);

        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.UserName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.PasswordHash)
            .IsRequired();

        builder.Property(u => u.ImgUrl)
            .HasMaxLength(255);

        builder.Property(u => u.Role)
            .IsRequired();

        builder.Property(u => u.IsActive)
            .IsRequired();

        builder.Property(u => u.CreatedAt).HasDefaultValueSql("GetDate()").ValueGeneratedOnAdd();

       
        builder.HasMany(u => u.PostComments)
            .WithOne(pc=>pc.User)
            .HasForeignKey(pc=>pc.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(u => u.Posts).WithOne(p=>p.Author)
            .HasForeignKey(p=>p.AuthorId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(u => u.Categories).WithOne(c => c.User)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.NoAction);

    }
}