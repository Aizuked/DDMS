using Core.Models.Facets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations.Facets;

public class FacetConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Facet
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        throw new NotImplementedException();
    }
}