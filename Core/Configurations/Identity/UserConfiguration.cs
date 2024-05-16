using Core.Models.Chats;
using Core.Models.Identity;
using Core.Models.Projects;
using Core.Models.Questionnaires;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations.Identity;

public class UserConfiguration<TEntity> : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(p => p.IsOnline)
               .IsRequired();

        builder.Property(p => p.LastOnline);

        builder.HasMany(p => p.UserChats)
               .WithMany(p => p.Participants);

        builder.HasMany(p => p.LocalFiles)
               .WithOne(p => p.Uploader)
               .HasForeignKey(p => p.UploaderId);

        builder.HasMany(p => p.Questionnaires)
               .WithOne(p => p.Author);

        builder.HasMany(p => p.QuestionnaireResults)
               .WithOne(p => p.Interviewee)
               .HasForeignKey(p => p.QuestionnaireId);

        builder.HasMany<Message>()
               .WithOne(p => p.Sender)
               .HasForeignKey(p => p.SenderId);

        builder.HasMany<Project>()
               .WithOne(p => p.Teacher)
               .HasForeignKey(p => p.TeacherId);

        builder.HasOne<Project>()
               .WithOne(p => p.Student)
               .HasForeignKey<Project>(p => p.StudentId);
    }
}