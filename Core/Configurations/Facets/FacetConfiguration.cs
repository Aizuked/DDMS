using Core.Models.Facets;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations.Facets;

public class FacetConfiguration : BaseEntityConfiguration<Facet>
{
    public new void Configure(EntityTypeBuilder<Facet> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.IsSystem)
               .IsRequired();

        builder.Property(p => p.Code)
               .HasMaxLength(128)
               .IsRequired();

        builder.Property(p => p.DisplayName)
               .HasMaxLength(256)
               .IsRequired();

        builder.Property(p => p.Description)
               .HasMaxLength(512)
               .IsRequired();

        builder.HasMany<FacetItem>(p => p.FacetItems)
               .WithOne();
    }
}