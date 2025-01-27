using Base.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Base.Data.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(_ => _.Id);

        builder.Property(_ => _.Id)
            .ValueGeneratedOnAdd()
            .IsRequired()
            .HasColumnName("Id");  

        builder.Property(_ => _.Email)
            .IsRequired()
            .HasColumnName("Email")
            .HasColumnType("varchar")
            .HasMaxLength(100);

        builder.Property(_ => _.Active)
            .IsRequired()
            .HasColumnName("Active");

        builder.Property(_ => _.Password)
            .IsRequired()
            .HasColumnName("Password")
            .HasColumnType("varchar")
            .HasMaxLength(256);

        builder.Property(_ => _.Role)
            .IsRequired()
            .HasColumnName("Role")
            .HasColumnType("varchar")
            .HasMaxLength(50);

        builder.HasOne(u => u.Customer)
            .WithOne(c => c.User)
            .HasForeignKey<User>(u => u.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
