﻿using Azure.Core;
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

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetAllEntries(CurrencyRepository repo) 
        {
            List<Currency> res = await repo.GetAllCurrencies();
            return TypedResults.Ok(res);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> CalculateAmount([FromServices] CurrencyRepository repo, [FromBody] ConvertionRequest request)
        {
            // PLACEHOLDER
            // While connection to actual db not functional this lets us test if the api works. 
            return TypedResults.Ok("50");
            

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
