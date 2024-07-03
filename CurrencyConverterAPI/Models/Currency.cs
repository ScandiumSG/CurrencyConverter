using System.ComponentModel.DataAnnotations;

namespace CurrencyConverterAPI.Models
{
    public class Currency
    {
        /// <summary>
        /// Three letter currency code
        /// </summary>
        [Key]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Relative value of the currency
        /// </summary>
        public decimal ExchangeRate { get; set; }
    }
}
