using Core.Models.Themes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations.Themes;

public class KeyWordConfiguration : BaseEntityConfiguration<KeyWord>
{
    public new void Configure(EntityTypeBuilder<KeyWord> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Word)
               .HasMaxLength(64)
               .IsRequired();

        builder.Property(p => p.IsApproved)
               .IsRequired();

        builder.Property(p => p.IsProven)
               .IsRequired();
    }
}