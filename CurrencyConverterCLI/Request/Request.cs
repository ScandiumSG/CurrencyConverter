using System.Globalization;

namespace CurrencyConverterCLI.Request
{

    public class Request
    {
        private string _currencyOne = "";
        private string _currencyTwo = "";
        private decimal _currencyAmount = 0;

        public string CurrencyOne 
        { 
            get { return _currencyOne.ToUpper(); } 
            set {if (ValidateCurrencyCode(value)) { _currencyOne = value; }} 
        }

        public string CurrencyTwo
        {
            get { return _currencyTwo.ToUpper(); }
            set { if (ValidateCurrencyCode(value)) { _currencyTwo = value; } }
        }

        public decimal CurrencyAmount 
        { 
            get { return _currencyAmount; } 
        }

        /// <summary>
        /// Transform the request object into a FormUrlEncodedContent object. 
        /// </summary>
        /// <returns>FormUrlEncodedContent containing CurrencyCode1, CurrencyCode2, and CurrencyAmount.</returns>
        public FormUrlEncodedContent GetPostBody() 
        {
            var values = new Dictionary<string, string>
            {
                { "CurrencyCode1", CurrencyOne},
                { "CurrencyCode2", CurrencyTwo},
                { "CurrencyAmount", CurrencyAmount.ToString()}
            };

            return new FormUrlEncodedContent(values);
        }

        /// <summary>
        /// Parse a string into a decimal value. If string was successfully parsed into decimal sets the value of CurrencyAmount for the Request object.
        /// </summary>
        /// <param name="value"> The string that is to be attempted to be transformed into a decimal.</param>
        /// <returns>Boolean, true if the string was successfully parsed into a decimal, false otherwise.</returns>
        public bool ValidateAndSetDecimal(string value) 
        {
            return decimal.TryParse(value, CultureInfo.InvariantCulture, out _currencyAmount);
        }

        /// <summary>
        /// Validate valid 3-character currency code. 
        /// </summary>
        /// <param name="value">The string to validate</param>
        /// <returns>Boolean, true if string fits criteria, false otherwise.</returns>
        private bool ValidateCurrencyCode(string value) 
        {
            if (value.Length == 3 && value.All(c => char.IsLetter(c)))
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
    }

}
