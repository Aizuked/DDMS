using Core.Models.Files;
using Core.Models.Identitiy;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations.Files;

public class LocalFileConfiguration : BaseEntityConfiguration<LocalFile>
{
    public new void Configure(EntityTypeBuilder<LocalFile> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.PhysicalPath)
               .HasMaxLength(512)
               .IsRequired();

        builder.Property(p => p.DisplayName)
               .HasMaxLength(256)
               .IsRequired();

        builder.Property(p => p.MimeType)
               .HasMaxLength(130)
               .IsRequired();

        builder.Property(p => p.Size)
               .IsRequired();

        builder.HasOne<User>(p => p.Uploader)
               .WithOne()
               .HasForeignKey<LocalFile>(p => p.UploaderId)
               .IsRequired();

        builder.HasOne<LocalFileGroup>(p => p.LocalFileGroup)
               .WithOne()
               .HasForeignKey<LocalFile>(p => p.LocalFileGroupId)
               .IsRequired();
    }
}