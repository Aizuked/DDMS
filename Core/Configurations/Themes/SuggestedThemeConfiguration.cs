using Core.Models.Themes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations.Themes;

public class SuggestedThemeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : SuggestedTheme
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
               .ValueGeneratedOnAdd();

        builder.Property(p => p.Text)
               .HasMaxLength(512)
               .IsRequired();

        builder.HasOne(p => p.User)
               .WithMany()
               .HasForeignKey(p => p.UserId)
               .IsRequired();
    }
}