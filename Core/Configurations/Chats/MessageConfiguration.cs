using Core.Models.Chats;
using Core.Models.Files;
using Core.Models.Identitiy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations.Chats;

public class MessageConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Message
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
               .ValueGeneratedOnAdd();

        builder.Property(p => p.Content)
               .HasMaxLength(2000);

        builder.Property(p => p.TimeStamp)
               .IsRequired();

        builder.Property(p => p.IsEdited)
               .IsRequired();

        builder.Property(p => p.IsReceived)
               .IsRequired();

        builder.Property(p => p.IsDeleted)
               .IsRequired();

        builder.HasOne<LocalFile>()
               .WithOne()
               .HasForeignKey<Message>(p => p.LocalFileId);

        builder.HasOne<User>(p => p.Sender)
               .WithOne()
               .HasForeignKey<Message>(p => p.SenderId)
               .IsRequired();
    }
}