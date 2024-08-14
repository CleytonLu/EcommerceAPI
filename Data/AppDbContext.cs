using Microsoft.EntityFrameworkCore;
using EcommerceAPI.Models;
using System.Reflection;
using System.Reflection.Emit;

namespace EcommerceAPI.Data
{
    public class AppDbContext : DbContext
    {
        // constructor
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        // propriedades de cada tabela
        public DbSet<Client> Clients { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Order> Orders { get; set; }

        // nomenclatura correta da tabela
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().ToTable("clients");
            modelBuilder.Entity<Seller>().ToTable("sellers");
            modelBuilder.Entity<Order>().ToTable("orders");
        }
    }
}