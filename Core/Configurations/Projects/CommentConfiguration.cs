using Core.Models.Identity;
using Core.Models.Projects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations.Projects;

public class CommentConfiguration : BaseEntityConfiguration<Comment>
{
    public new void Configure(EntityTypeBuilder<Comment> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Text)
               .HasMaxLength(512)
               .IsRequired();

        builder.Property(p => p.IsPrivate)
               .IsRequired();

        builder.HasOne<User>(p => p.Author)
               .WithOne()
               .IsRequired();
    }
}