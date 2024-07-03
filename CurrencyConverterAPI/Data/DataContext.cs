using CurrencyConverterAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConverterAPI.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Define primary key
            modelBuilder.Entity<Currency>().HasIndex(c => c.CurrencyCode);
        }

        public DbSet<Currency> Currencies { get; set; }
    }
}
