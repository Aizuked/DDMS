using Core.Models.Projects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations.Projects;

public class CommentConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Comment
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        throw new NotImplementedException();
    }
}