using CurrencyConverterAPI.Models;
using CurrencyConverterAPI.Repository;
using System.Globalization;
using System.Text.Json;

namespace CurrencyConverterAPI.Services
{
    public class FetchingService: IDisposable
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
            // Define the function that is called, FetchData, the delay, 0, and the time until it repeats, 24h.
            _timer = new Timer((async _ => await FetchData()), null, TimeSpan.Zero, TimeSpan.FromHours(24));
        }

        /// <summary>
        /// Dispose of the HttpClient and Timer objects when fetching no longer required.
        /// </summary>
        public void Dispose()
        {
            _httpClient.Dispose();
            _timer.Dispose();
        }

        /// <summary>
        /// Initialize a update of the currency values immediately
        /// </summary>
        /// <returns></returns>
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
