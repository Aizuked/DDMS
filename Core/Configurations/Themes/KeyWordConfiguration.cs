using Core.Models.Themes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations.Themes;

public class KeyWordConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : KeyWord
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        throw new NotImplementedException();
    }
}