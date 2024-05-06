using Core.Models.Questionnaires;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations.Questionnaires;

public class QuestionnaireConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Questionnaire
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        throw new NotImplementedException();
    }
}