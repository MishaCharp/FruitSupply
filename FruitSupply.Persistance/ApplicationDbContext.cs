
using FruitSupply.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace FruitSupply.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        private static ApplicationDbContext instance;
        public static ApplicationDbContext Instance
        {
            get => instance ?? (instance = new ApplicationDbContext());
        } 

        public DbSet<Product> Product { get; set; }
        public DbSet<ProductGrade> ProductGrade { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<SupplierPrice> SupplierPrice { get; set; }
        public DbSet<Supply> Supply { get; set; }
        public DbSet<SupplyDetail> SupplyDetail { get; set; }
        public DbSet<Unit> Unit { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=stud-mssql.sttec.yar.ru,38325;Database=user300_db;User Id=user300_db; Password=user300;TrustServerCertificate=True");
        }

    }
}
