namespace CurrencyConverterAPI.Models
{
    public class Currency
    {
        // Three letter currency code
        public string CurrencyCode { get; set; }

        // Relative value of the currency
        public decimal ExchangeRate { get; set; }
    }
}
