using Base.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Base.Data.Configuration;

public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.ToTable("Wallets");

        builder.HasKey(_ => _.Id);

        builder.Property(_ => _.Id)
            .ValueGeneratedOnAdd()
            .IsRequired()
            .HasColumnName("Id");

        builder.Property(_ => _.WalletNumber)
            .IsRequired()
            .HasColumnName("WalletNumber");

        builder.Property(_ => _.Balance)
            .IsRequired()
            .HasColumnName("Balance")
            .HasColumnType("decimal(18,2)");

        builder.HasOne(w => w.Customer)
              .WithOne(c => c.CustomerWallet)
              .HasForeignKey<Wallet>(w => w.CustomerId)
              .OnDelete(DeleteBehavior.Cascade);
    }
}
