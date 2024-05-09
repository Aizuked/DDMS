using Core.Models.Chats;
using Core.Models.Files;
using Core.Models.Identitiy;
using Core.Models.Questionnaires;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations.Identity;

public class UserConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : User
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(p => p.IsOnline)
               .IsRequired();

        builder.Property(p => p.LastOnline);

        builder.HasOne<LocalFile>(p => p.ProfilePicture)
               .WithOne()
               .HasForeignKey<User>(p => p.ProfilePictureId);

        builder.HasMany<Chat>(p => p.UserChats)
               .WithOne();

        builder.HasMany<LocalFile>(p => p.LocalFiles)
               .WithOne();

        builder.HasMany<Questionnaire>(p => p.Questionnaires)
               .WithOne();
    }
}