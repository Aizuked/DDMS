using Core.Models.Questionnaires;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations.Questionnaires;

public class QuestionConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Question
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        throw new NotImplementedException();
    }
}