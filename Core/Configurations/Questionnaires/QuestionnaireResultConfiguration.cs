using Core.Models.Identitiy;
using Core.Models.Questionnaires;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations.Questionnaires;

public class QuestionnaireResultConfiguration : BaseEntityConfiguration<QuestionnaireResult>
{
    public new void Configure(EntityTypeBuilder<QuestionnaireResult> builder)
    {
        base.Configure(builder);

        builder.HasOne<User>(p => p.Interviewee)
               .WithOne()
               .HasForeignKey<QuestionnaireResult>(p => p.QuestionnaireId)
               .IsRequired();

        builder.HasOne<Questionnaire>(p => p.Questionnaire)
               .WithOne()
               .HasForeignKey<QuestionnaireResult>(p => p.QuestionnaireId)
               .IsRequired();

        builder.HasMany<Answer>(p => p.Answers)
               .WithOne();
    }
}