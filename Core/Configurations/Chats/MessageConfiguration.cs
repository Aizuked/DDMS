﻿using Core.Models.Chats;
using Core.Models.Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations.Chats;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
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

        builder.HasOne(p => p.LocalFile)
               .WithOne()
               .HasForeignKey<Message>(p => p.LocalFileId);

        builder.HasOne(p => p.ProjectTask)
               .WithMany();

        builder.HasOne(p => p.Sender)
               .WithMany()
               .HasForeignKey(p => p.SenderId);
    }
}