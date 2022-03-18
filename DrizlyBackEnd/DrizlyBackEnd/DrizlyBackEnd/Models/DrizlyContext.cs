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
    }
}
