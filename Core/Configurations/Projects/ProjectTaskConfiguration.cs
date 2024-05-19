using Core.Models.Facets;
using Core.Models.Identity;
using Core.Models.Projects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations.Projects;

public class ProjectTaskConfiguration : BaseEntityConfiguration<ProjectTask>
{
    public new void Configure(EntityTypeBuilder<ProjectTask> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.DisplayName)
               .HasMaxLength(256)
               .IsRequired();

        builder.Property(p => p.Description)
               .HasMaxLength(2048);

        builder.Property(p => p.Readiness)
               .IsRequired();

        builder.Property(p => p.DateTimeStart);

        builder.Property(p => p.DateTimeEnd);

        builder.HasOne(p => p.ParentTask)
               .WithMany()
               .HasForeignKey(p => p.ParentTaskId);

        builder.HasOne(p => p.Status)
               .WithMany()
               .HasForeignKey(p => p.StatusId)
               .IsRequired();

        builder.HasOne(p => p.Author)
               .WithMany()
               .HasForeignKey(p => p.AuthorId)
               .IsRequired();

        builder.HasMany(p => p.Comments)
               .WithOne();

        builder.HasMany(p => p.LinkedTasks)
               .WithMany();

        builder.HasMany(p => p.LocalFiles)
               .WithOne();
    }
}