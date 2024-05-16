using Core.Models.Facets;
using Core.Models.Questionnaires;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations.Questionnaires;

public class QuestionConfiguration<TEntity> : BaseEntityConfiguration<Question>
{
    public new void Configure(EntityTypeBuilder<Question> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Text)
               .HasMaxLength(512)
               .IsRequired();

        builder.Property(p => p.IsRequired)
               .IsRequired();

        builder.HasOne<FacetItem>(p => p.Type)
               .WithMany()
               .HasForeignKey(p => p.TypeId)
               .IsRequired();
    }
}