using Core.Models.Chats;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations.Chats;

public class ChatConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Chat
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        throw new NotImplementedException();
    }
}