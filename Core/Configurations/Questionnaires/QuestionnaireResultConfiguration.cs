using Core.Models.Questionnaires;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations.Questionnaires;

public class QuestionnaireResultConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : QuestionnaireResult
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        throw new NotImplementedException();
    }
}