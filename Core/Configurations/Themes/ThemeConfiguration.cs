using Core.Models.Identitiy;
using Core.Models.Themes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations.Themes;

public class ThemeConfiguration : BaseEntityConfiguration<Theme>
{
    public new void Configure(EntityTypeBuilder<Theme> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.IsProcessed)
               .IsRequired();

        builder.Property(p => p.IsApproved)
               .IsRequired();

        builder.Property(p => p.IsChangeRequested)
               .IsRequired();

        builder.HasOne<SuggestedTheme>(p => p.SelectedTheme)
               .WithOne()
               .HasForeignKey<Theme>(p => p.SelectedThemeId);

        builder.HasOne<User>(p => p.Approver)
               .WithOne()
               .HasForeignKey<Theme>(p => p.ApproverId);

        builder.HasMany<SuggestedTheme>(p => p.SuggestedThemes)
               .WithOne();

        builder.HasMany<KeyWord>(p => p.KeyWords)
               .WithOne();
    }
}