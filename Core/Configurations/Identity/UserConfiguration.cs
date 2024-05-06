using Core.Models.Identitiy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations.Identity;

public class UserConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : User
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        throw new NotImplementedException();
    }
}