using Base.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Base.Data.Configuration;

public class BlockchainConfiguration : IEntityTypeConfiguration<Blockchain>
{
    public void Configure(EntityTypeBuilder<Blockchain> builder)
    {
        builder.ToTable("Blockchains");

        builder.HasKey(_ => _.Id);

        builder.Property(_ => _.Id)
            .ValueGeneratedOnAdd()
            .IsRequired()
            .HasColumnName("Id");

        builder.Property(_ => _.Value)
            .IsRequired()
            .HasColumnName("Value")
            .HasColumnType("decimal(18,2)");

        builder.Property(_ => _.TransferData)
            .IsRequired()
            .HasColumnName("TransferData");
       
    }
}
