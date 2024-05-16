using Core.Models.Facets;
using Core.Models.Projects;
using Core.Models.Themes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations.Projects;

public class ProjectConfiguration : BaseEntityConfiguration<Project>
{
    public new void Configure(EntityTypeBuilder<Project> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Code)
               .HasMaxLength(128)
               .IsRequired();

        builder.Property(p => p.DisplayName)
               .HasMaxLength(256)
               .IsRequired();

        builder.Property(p => p.IsPublic)
               .IsRequired();

        builder.HasOne(p => p.Student)
               .WithOne()
               .HasForeignKey<Project>(p => p.StudentId)
               .IsRequired();

        builder.HasOne(p => p.Teacher)
               .WithMany()
               .HasForeignKey(p => p.TeacherId)
               .IsRequired();

        builder.HasOne(p => p.Status)
               .WithMany()
               .HasForeignKey(p => p.StatusId)
               .IsRequired();

        builder.HasOne(p => p.Theme)
               .WithOne()
               .HasForeignKey<Project>(p => p.ThemeId);
    }
}