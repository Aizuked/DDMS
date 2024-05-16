using Core.Models.Chats;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations.Chats;

public class ChatConfiguration : BaseEntityConfiguration<Chat>
{
    public new void Configure(EntityTypeBuilder<Chat> builder)
    {
        base.Configure(builder);

        builder.HasOne(p => p.Project)
               .WithOne()
               .HasForeignKey<Chat>(p => p.ProjectId);

        builder.HasMany(p => p.Messages)
               .WithMany();

        builder.HasMany(p => p.Participants)
               .WithMany(p => p.UserChats);
    }
}