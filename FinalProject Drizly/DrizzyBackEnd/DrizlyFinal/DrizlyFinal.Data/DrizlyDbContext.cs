using DrizlyFinal.Core.Models;
using DrizlyFinal.Data.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrizlyFinal.Data
{
    public class DrizlyDbContext : IdentityDbContext
    {
        public DrizlyDbContext(DbContextOptions<DrizlyDbContext> options) : base(options)
        {
        }
        public DbSet<AppUser> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
    public class YourDbContextFactory : IDesignTimeDbContextFactory<DrizlyDbContext>
    {
        public DrizlyDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DrizlyDbContext>();
            optionsBuilder.UseSqlServer("Default");

            return new DrizlyDbContext(optionsBuilder.Options);
        }
    }
}
