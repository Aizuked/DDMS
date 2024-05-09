using Core.Models.Files;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations.Files;

public class LocalFileGroupConfiguration : BaseEntityConfiguration<LocalFileGroup>
{
    public new void Configure(EntityTypeBuilder<LocalFileGroup> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Code)
               .HasMaxLength(128)
               .IsRequired();

        builder.Property(p => p.DisplayName)
               .HasMaxLength(256)
               .IsRequired();

        builder.Property(p => p.Description)
               .HasMaxLength(512)
               .IsRequired();

        builder.Property(p => p.MaxSize)
               .IsRequired();

        builder.Property(p => p.AllowedMimeTypes);

        builder.HasMany<LocalFile>(p => p.Files)
               .WithOne();
    }
}