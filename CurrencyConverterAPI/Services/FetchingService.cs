namespace CurrencyConverterAPI.Services
{
    public class FetchingService
    {
        private Timer _timer;
        private HttpClient _httpClient;
        private string _apiUrl;

        public FetchingService (String token) 
        {
            // Fixer documentation for accessing data https://fixer.io/documentation
            // Cant use https with free tier
            _apiUrl = "http://data.fixer.io/api/latest?access_key=" + token + "&format=1";
            _httpClient = new HttpClient();
            //_timer = new Timer(null, null, 0, 24*60*60);
        }

        public async Task UpdateNow() 
        {
            await FetchData();
        }

        private async Task FetchData() 
        {
            var response = await _httpClient.GetAsync(_apiUrl);
            var content = response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
        }
    }
}
