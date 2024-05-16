using Core.Models.Identity;
using Core.Models.Questionnaires;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations.Questionnaires;

public class QuestionnaireResultConfiguration : BaseEntityConfiguration<QuestionnaireResult>
{
    public new void Configure(EntityTypeBuilder<QuestionnaireResult> builder)
    {
        base.Configure(builder);

        builder.HasOne<User>(p => p.Interviewee)
               .WithMany()
               .HasForeignKey(p => p.IntervieweeId)
               .IsRequired();

        builder.HasOne<Questionnaire>(p => p.Questionnaire)
               .WithMany()
               .HasForeignKey(p => p.QuestionnaireId)
               .IsRequired();

        builder.HasMany<Answer>(p => p.Answers)
               .WithOne();
    }
}