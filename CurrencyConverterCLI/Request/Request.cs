using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverterCLI.Request
{

    public class Request
    {
        private string _currencyOne;
        private string _currencyTwo;
        private decimal _currencyAmount;

        public string CurrencyOne { get; set; }

        public string CurrencyTwo { get; set; }

        public decimal CurrencyAmount { 
            get { return _currencyAmount; } 
            set { _currencyAmount = value; } 
        }

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
    }

}
