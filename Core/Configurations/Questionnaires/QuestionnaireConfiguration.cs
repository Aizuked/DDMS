using Core.Models.Facets;
using Core.Models.Identity;
using Core.Models.Questionnaires;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations.Questionnaires;

public class QuestionnaireConfiguration : BaseEntityConfiguration<Questionnaire>
{
    public new void Configure(EntityTypeBuilder<Questionnaire> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.DisplayName)
               .HasMaxLength(128)
               .IsRequired();

        builder.Property(p => p.ParticipationCount)
               .IsRequired();

        builder.HasOne<FacetItem>(p => p.Type)
               .WithMany()
               .HasForeignKey(p => p.TypeId)
               .IsRequired();

        builder.HasOne<User>(p => p.Author)
               .WithMany()
               .HasForeignKey(p => p.AuthorId)
               .IsRequired();

        builder.HasMany<Question>(p => p.Questions)
               .WithOne();
    }
}