using CurrencyConverterAPI.Models;
using CurrencyConverterAPI.Repository;
using System.Globalization;
using System.Text.Json;

namespace CurrencyConverterAPI.Services
{
    public class FetchingService
    {
        private Timer _timer;
        private HttpClient _httpClient;
        private string _apiUrl;
        private CurrencyRepository _currencyRepo;

        public FetchingService (String token, CurrencyRepository currencyRepo) 
        {
            // Fixer documentation for accessing data https://fixer.io/documentation
            // Cant use https with free tier
            _apiUrl = "http://data.fixer.io/api/latest?access_key=" + token + "&format=1";
            _httpClient = new HttpClient();
            _currencyRepo = currencyRepo;
            //_timer = new Timer(null, null, 0, 24*60*60);
        }

        public async Task UpdateNow() 
        {
            await FetchData();
        }

        private async Task FetchData() 
        {
            try
            {
                var response = await _httpClient.GetAsync(_apiUrl);
                response.EnsureSuccessStatusCode();

                // Read the content of response
                string contentString = await response.Content.ReadAsStringAsync();
                // Convert from json string to usable objects
                var jsonData = JsonDocument.Parse(contentString);
                JsonElement currencyRates = jsonData.RootElement.GetProperty("rates");

                List<Currency> convertedCurrencies = currencyRates.EnumerateObject()
                    .Select(c =>
                        new Currency() {
                            CurrencyCode = c.Name,
                            ExchangeRate = decimal.Parse(
                                c.Value.ToString(),
                                NumberStyles.Float | NumberStyles.AllowExponent,
                                CultureInfo.InvariantCulture
                            )
                        }
                    )
                    .ToList();

                await _currencyRepo.UpdateCurrencyExchange(convertedCurrencies);
            }
            catch (HttpRequestException e)
            {
                // TODO: Add propper logger
                Console.WriteLine(e);
            }
            catch (JsonException e) 
            {
                // TODO: Add proper logger
                Console.WriteLine(e);
            }
        }
    }
}
