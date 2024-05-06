using Core.Models.Projects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations.Projects;

public class ProjectConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Project
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        throw new NotImplementedException();
    }
}