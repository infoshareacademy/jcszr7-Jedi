﻿using JediApp.Database.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NuGet.Protocol.Plugins;

namespace JediApp.Web.Areas.Identity.Data;

public class JediAppDbContext : IdentityDbContext<User>
{
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