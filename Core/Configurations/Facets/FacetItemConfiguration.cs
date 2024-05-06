using Core.Models.Facets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations.Facets;

public class FacetItemConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : FacetItem
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        throw new NotImplementedException();
    }
}