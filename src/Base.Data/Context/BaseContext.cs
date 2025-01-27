using Base.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Base.Data.Context;

public class BaseContext : DbContext
{
    public BaseContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=P@ssw0rd");
    }

    public DbSet<Customer> Customers {  get; set; }
    public DbSet<Wallet> Wallets {  get; set; }
    public DbSet<Blockchain> Blockchains {  get; set; }
    public DbSet<User> Users {  get; set; }
}
