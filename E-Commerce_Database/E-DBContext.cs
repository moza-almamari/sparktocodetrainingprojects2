using System;
using System.Collections.Generic;
using System.Text;
using E_Commerce_Database.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Database
{
    public class E_DBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=MOZA-PC\\SQLEXPRESS;Database=EFCoreProject;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False;");
        }
    }
}
