using System;
using System.Collections.Generic;
using System.Text;
using EFCoreProject.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreProject
{
    public class ProjectContext : DbContext //oop inhertance 
    {
        //1- register models 
        public DbSet<Employee> employees {  get; set; }
        public DbSet<Department> departments { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=MOZA-PC\\SQLEXPRESS;Database=EFCoreProject;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False;");
        }

    }
}
