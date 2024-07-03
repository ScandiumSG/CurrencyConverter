// See https://aka.ms/new-console-template for more information

using CurrencyConverterCLI.Request;
using System.Net.Http.Json;

HttpClient client = new HttpClient();
string defaultUrl = "http://localhost:5143";
Request req = new Request();

while (true) 
{
    Console.WriteLine("Enter the currency code you wish to convert from: ");
    while (req.CurrencyOne is "") 
    {
        req.CurrencyOne = Console.ReadLine();
        if (req.CurrencyOne is "") 
        {
            Console.WriteLine("Invalid currency. Can only be a 3 character currency code.");
        }
    }

    Console.WriteLine("Enter the currency code you with to convert to: ");
    req.CurrencyTwo = Console.ReadLine();
    while (req.CurrencyTwo is "")
    {
        req.CurrencyTwo = Console.ReadLine();
        if (req.CurrencyTwo is "")
        {
            Console.WriteLine("Invalid currency. Can only be a 3 character currency code.");
        }
    }

    Console.WriteLine($"Input the amount of {req.CurrencyOne} you wish to convert to {req.CurrencyTwo}:");
    while (true) 
    {
        string inputAmount = Console.ReadLine();
        if (!req.ValidateAndSetDecimal(inputAmount))
        {
            Console.WriteLine("Invalid amount, please only a digit");
        }
        else 
        {
            break;
        }

    }

    var content = await client.PostAsJsonAsync(defaultUrl, req.GetPostBody());
    if (content.IsSuccessStatusCode)
    {
        var result = await content.Content.ReadAsStringAsync();
        decimal resultNumber = decimal.Parse(result.Trim('"'));
        Console.WriteLine($"You entered {req.CurrencyAmount} {req.CurrencyOne}, which is equal to {resultNumber} {req.CurrencyTwo}");
    }
    else 
    {
        Console.WriteLine(await content.Content.ReadAsStringAsync());
    }

    Console.WriteLine("Press 'y' to perform another conversion, or any other key to exit.");
    var input = Console.ReadLine();
    if (input.Equals("y"))
    {
        req = new Request();
        Console.WriteLine("Convert another currency:");
    }
    else 
    {
        Console.WriteLine("Exiting program.");
        break;
    }
}
