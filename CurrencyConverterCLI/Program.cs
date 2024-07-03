// See https://aka.ms/new-console-template for more information

using CurrencyConverterCLI.Request;
using System.Net.Http.Json;

HttpClient client = new HttpClient();
string defaultUrl = "https://localhost:7133";
string currencyCode1;
string currencyCode2;

Request req = new Request();

while (true) 
{
    Console.WriteLine("Enter the currency code you wish to convert from: ");
    req.CurrencyOne = Console.ReadLine();

    Console.WriteLine("Enter the currency code you with to convert to: ");
    req.CurrencyTwo = Console.ReadLine();

    Console.WriteLine($"Input the amount of {req.CurrencyOne} you wish to convert to ${req.CurrencyTwo}:");
    req.CurrencyAmount = decimal.Parse(Console.ReadLine());

    var content = await client.PostAsJsonAsync(defaultUrl, req.GetPostBody());
    if (content.IsSuccessStatusCode)
    {
        var result = await content.Content.ReadAsStringAsync();
        Console.WriteLine($"You entered ${req.CurrencyAmount} {req.CurrencyOne}, which is equal to ${result}");
    }
    else 
    {
        Console.WriteLine(await content.Content.ReadAsStringAsync());
    }
}
