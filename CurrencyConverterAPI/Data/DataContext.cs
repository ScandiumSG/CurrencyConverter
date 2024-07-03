using CurrencyConverterAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConverterAPI.Data
{
    public class DataContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Define primary key
            modelBuilder.Entity<Currency>().HasIndex(c => c.CurrencyCode);
        }

    }
}
