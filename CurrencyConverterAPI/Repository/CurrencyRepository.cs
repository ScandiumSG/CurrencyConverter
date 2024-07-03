using CurrencyConverterAPI.Data;
using CurrencyConverterAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConverterAPI.Repository
{
    public class CurrencyRepository
    {
        private protected DataContext _context;
        private protected DbSet<Currency> _dbSet;

        public CurrencyRepository(DataContext context) 
        {
            _context = context;
            _dbSet = context.Set<Currency>();
        }

        /// <summary>
        /// Retrieve a specific currency object from the database
        /// </summary>
        /// <param name="currencyCode">Three letter currency string</param>
        /// <returns> Currency object</returns>
        public async Task<Currency?> GetCurrency(string currencyCode) 
        {
            Currency? currency = await _dbSet.FindAsync(currencyCode);
            return currency;
        }

        /// <summary>
        /// Update a range of currency values. 
        /// </summary>
        /// <param name="currencies">List of currency objects</param>
        /// <returns>The list of updated values</returns>
        public async Task<List<Currency>> UpdateCurrencyExchange(List<Currency> currencies) 
        {
            _dbSet.UpdateRange(currencies);
            await _context.SaveChangesAsync();
            return currencies;
        }
    }
}
