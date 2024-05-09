using Core.Models.Questionnaires;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations.Questionnaires;

public class AnswerConfiguration : BaseEntityConfiguration<Answer>
{
    public new void Configure(EntityTypeBuilder<Answer> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Text)
               .HasMaxLength(128);

        builder.Property(p => p.DateTimeStart);

        builder.Property(p => p.DateTimeEnd);

        builder.Property(p => p.Number);

        builder.Property(p => p.Checked);

        builder.Property(p => p.MultiSelected);

        builder.HasOne<Question>(p => p.Question)
               .WithOne()
               .HasForeignKey<Answer>(p => p.QuestionId)
               .IsRequired();
    }
}