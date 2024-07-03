namespace CurrencyConverterAPI.Models
{
    public class ConvertionRequest
    {
        /// <summary>
        /// The currency code to convert from. Must be a string of 3-characters
        /// </summary>
        public string CurrencyCode1 { get; set; }

        /// <summary>
        /// The currency code to convert to. Must be a string of 3-characters
        /// </summary>
        public string CurrencyCode2 { get; set; }

        /// <summary>
        /// The amount of CurrencyCode1 to convert into CurrencyCode2
        /// </summary>
        public decimal CurrencyAmount { get; set; }
    }
}
