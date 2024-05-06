using Core.Models.Chats;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations.Chats;

public class MessageConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Message
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        throw new NotImplementedException();
    }
}