using Microsoft.EntityFrameworkCore;
using SimpleControlDesktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleControlDesktop.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Desktop> Desktops { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<UserPhone> UserPhones { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<Notification> Notifications { get; set; }

      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Connection>().HasKey(e => new { e.DesktopId, e.UserPhoneId });
        }
    }
}
