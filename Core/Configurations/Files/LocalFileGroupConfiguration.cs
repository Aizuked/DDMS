using Core.Models.Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations.Files;

public class LocalFileGroupConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : LocalFileGroup
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        throw new NotImplementedException();
    }
}