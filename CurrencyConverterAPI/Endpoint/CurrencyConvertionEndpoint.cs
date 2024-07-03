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
            app.MapPost("/", CalculateAmount);
        }

        public static async Task<IResult> CalculateAmount(CurrencyRepository repo, [FromQuery] ConvertionRequest request)
        {
            Currency? Cur1 = await repo.GetCurrency(request.CurrencyCode1);
            Currency? Cur2 = await repo.GetCurrency(request.CurrencyCode2);

            if (Cur1 is null || Cur2 is null)
            {
                return TypedResults.BadRequest("Invalid currency codes");
            }

            decimal ConvertedCurrencyAmount = request.CurrencyAmount * (Cur2.ExchangeRate / Cur1.ExchangeRate);
            return TypedResults.Ok(ConvertedCurrencyAmount);
        }
    }
}
