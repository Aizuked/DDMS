using Core.Models.Facets;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations.Facets;

public class FacetItemConfiguration : BaseEntityConfiguration<FacetItem>
{
    public new void Configure(EntityTypeBuilder<FacetItem> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Code)
               .HasMaxLength(128)
               .IsRequired();

        builder.Property(p => p.DisplayName)
               .HasMaxLength(256)
               .IsRequired();

        builder.Property(p => p.Description)
               .HasMaxLength(512)
               .IsRequired();

        builder.HasOne<Facet>(p => p.Facet)
               .WithMany(p => p.FacetItems)
               .HasForeignKey(p => p.FacetId)
               .IsRequired();
    }
}