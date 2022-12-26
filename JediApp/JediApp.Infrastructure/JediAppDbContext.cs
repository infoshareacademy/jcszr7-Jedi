using JediApp.Database.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using System;


namespace JediApp.Web.Areas.Identity.Data;

public class JediAppDbContext : IdentityDbContext<User>
{

    public DbSet<User> Users { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<WalletPosition> WalletPositions { get; set; }
    public DbSet<Currency> Currencys { get; set; }
    public DbSet<TransactionHistory> TransactionHistory { get; set; }
    public DbSet<ExchangeOffice> ExchangeOffices { get; set; }
    public DbSet<ExchangeOfficeBoard> ExchangeOfficeBoards { get; set; }
    public DbSet<MoneyOnStock> MoneyOnStocks { get; set; }

    public JediAppDbContext()
    {

    }

    public JediAppDbContext(DbContextOptions<JediAppDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS; Database = JediDataTest; Trusted_Connection = true; ");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());

        {
            builder.Entity<Currency>()
            .HasOne<WalletPosition>(p => p.WalletPosition)
            .WithOne(pp => pp.Currency)
            .HasForeignKey<WalletPosition>(pp => pp.CurrencyId);
        }
        {
            builder.Entity<ExchangeOffice>()
            .HasOne<ExchangeOfficeBoard>(p => p.ExchangeOfficeBoard)
            .WithOne(pp => pp.ExchangeOffice)
            .HasForeignKey<ExchangeOfficeBoard>(pp => pp.ExchangeOfficeId);
        }
        {
            builder.Entity<Wallet>()
            .HasOne<User>(p => p.User)
            .WithOne(pp => pp.Wallet)
            .HasForeignKey<User>(pp => pp.WalletId);
        }


    }
}

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<User>
{
   
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u=>u.FirstName).HasMaxLength(50);
        builder.Property(u=>u.LastName).HasMaxLength(50);
    }
}