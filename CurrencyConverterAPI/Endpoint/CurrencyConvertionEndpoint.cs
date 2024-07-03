using Azure.Core;
using CurrencyConverterAPI.Models;
using CurrencyConverterAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverterAPI.Endpoint
{
    public static class CurrencyConvertionEndpoint
    {
        public static void CurrencyConvertionEndpointConfiguration(this WebApplication app) 
        {
            app.MapGet("/", GetAllEntries);
            app.MapPost("/", CalculateAmount);
        }

        public static async Task<IResult> GetAllEntries(CurrencyRepository repo) 
        {
            List<Currency> res = await repo.GetAllCurrencies();
            return TypedResults.Ok(res);
        }

        public static async Task<IResult> CalculateAmount([FromServices] CurrencyRepository repo, [FromBody] ConvertionRequest request)
        {
            if (request.CurrencyCode1 is null || request.CurrencyCode2 is null) 
            {
                return TypedResults.BadRequest("Currencies are not properly specified");
            }
            
            Currency? Cur1 = await repo.GetCurrency(request.CurrencyCode1.ToUpper());
            Currency? Cur2 = await repo.GetCurrency(request.CurrencyCode2.ToUpper());

            if (Cur1 is null || Cur2 is null)
            {
                return TypedResults.BadRequest("Invalid currency codes");
            }

            // Calculate the amount of currency2
            decimal ConvertedCurrencyAmount = request.CurrencyAmount * (Cur2.ExchangeRate / Cur1.ExchangeRate);
            return TypedResults.Ok(ConvertedCurrencyAmount);
        }
    }
}
