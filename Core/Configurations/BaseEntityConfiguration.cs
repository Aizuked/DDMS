using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Configurations;

public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(k => k.Id);

        builder.Property<int>(p => p.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property<bool>(p => p.IsDeleted)
            .HasColumnName("is_deleted")
            .IsRequired();

        builder.Property<DateTimeOffset>(p => p.Created)
            .HasColumnName("created")
            .IsRequired();

        builder.Property<DateTimeOffset>(p => p.Updated)
            .HasColumnName("updated")
            .IsRequired();
    }
}