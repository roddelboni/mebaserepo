using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Base.Domain.Entities;
using Base.Domain.Tools;


namespace Base.Data.Configuration;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(_ => _.Id);

        builder.Property(_ => _.Id)
            .ValueGeneratedOnAdd()
            .IsRequired()
            .HasColumnName("Id");

        builder.Property(_ => _.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("varchar")
            .HasMaxLength(100);

        builder.Property(_ => _.Document)
            .IsRequired()
            .HasColumnName("Document")
            .HasColumnType("varchar")
            .HasMaxLength(50);

        builder.Property(_ => _.BirthDate)
            .IsRequired()
            .HasColumnName("BirthDate");

        builder.HasOne(c => c.CustomerWallet)
             .WithOne(w => w.Customer)
             .HasForeignKey<Wallet>(w => w.CustomerId)
             .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(c => c.User)
            .WithOne(u => u.Customer)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
