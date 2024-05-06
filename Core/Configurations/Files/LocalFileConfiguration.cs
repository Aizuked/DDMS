using Core.Models.Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations.Files;

public class LocalFileConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : LocalFile
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        throw new NotImplementedException();
    }
}