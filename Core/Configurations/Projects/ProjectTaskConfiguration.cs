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

        builder.HasOne<ProjectTask>(p => p.ParentTask)
               .WithMany()
               .HasForeignKey(p => p.ParentTaskId);

        builder.HasOne<FacetItem>(p => p.Status)
               .WithMany()
               .HasForeignKey(p => p.StatusId)
               .IsRequired();

        builder.HasOne<User>(p => p.Author)
               .WithMany()
               .HasForeignKey(p => p.AuthorId)
               .IsRequired();

        builder.HasMany<Comment>(p => p.Comments)
               .WithOne();

        builder.HasMany<ProjectTask>(p => p.LinkedTasks)
               .WithMany();
    }
}