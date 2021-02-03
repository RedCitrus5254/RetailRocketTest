using Microsoft.EntityFrameworkCore;
using RetailRocketTest.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetailRocketTest
{
    class AppDBContent : DbContext
    {
        public AppDBContent()
        {
            Database.EnsureCreated();
        }

        public DbSet<Shop> Shops { get; set; }
        public DbSet<Product> Products { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RetailRocketTestDB;Trusted_Connection=True;");
        }
    }
}
