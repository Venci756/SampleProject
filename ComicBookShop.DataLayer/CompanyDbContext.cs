using System;
//using EFDbFirstApproachExample.Migrations;
using System.Data.Entity;
using ComicBookShop.DomainModels;

namespace ComicBookShop.DataLayer
{
    public class CompanyDbContext : DbContext
    {
        public CompanyDbContext() : base("MyConnectionString")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<CompanyDbContext, Configuration>());
        }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
