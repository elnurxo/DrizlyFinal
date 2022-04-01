using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Models
{
    public class DrizlyContext : IdentityDbContext
    {
        public DrizlyContext(DbContextOptions<DrizlyContext> options) : base(options)
        {
        }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Partnership> Partnerships { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<ProductSize> ProductSize { get; set; }
        public DbSet<ProductCount> ProductCount { get; set; }
        public DbSet<TypeProduct> TypeProducts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SweetDryScale> sweetDryScales { get; set; }
        public DbSet<LiquorFlavor> LiquorFlavors { get; set; }
        public DbSet<LiquorColor> LiquorColors { get; set; }
        public DbSet<WineFoodPairing> WineFoodPairings { get; set; }
        public DbSet<ProductFoodPairing> ProductFoodPairings { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
