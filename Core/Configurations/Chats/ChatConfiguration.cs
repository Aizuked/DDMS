using Core.Models.Chats;
using Core.Models.Identitiy;
using Core.Models.Projects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations.Chats;

public class ChatConfiguration : BaseEntityConfiguration<Chat>
{
    public new void Configure(EntityTypeBuilder<Chat> builder)
    {
        base.Configure(builder);

        builder.HasOne<Project>(p => p.Project)
               .WithOne()
               .HasForeignKey<Chat>(p => p.ProjectId);

        builder.HasMany<Message>(p => p.Messages)
               .WithOne();

        builder.HasMany<User>(p => p.Participants)
               .WithOne();
    }
}