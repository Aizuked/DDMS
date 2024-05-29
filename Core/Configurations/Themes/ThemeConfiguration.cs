using Core.Models.Identity;
using Core.Models.Themes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations.Themes;

public class ThemeConfiguration : BaseEntityConfiguration<Theme>
{
    public new void Configure(EntityTypeBuilder<Theme> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.IsApproved)
               .IsRequired();

        builder.HasOne<SuggestedTheme>(p => p.SelectedTheme)
               .WithOne();

        builder.HasOne<SuggestedTheme>(p => p.SelectedThemeToChange)
               .WithOne();

        builder.HasOne<User>(p => p.Approver)
               .WithMany()
               .HasForeignKey(p => p.ApproverId);
    }
}