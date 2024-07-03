using CurrencyConverterAPI.Data;
using CurrencyConverterAPI.Endpoint;
using CurrencyConverterAPI.Repository;
using CurrencyConverterAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>
    (
        opt => opt.UseCosmos("https://localhost:8081", "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==", databaseName: "CosmosDb")
    );

builder.Services.AddScoped<CurrencyRepository, CurrencyRepository>();

builder.Services.AddScoped<FetchingService>(provider =>
{
    var token = builder.Configuration.GetConnectionString("fixerToken")!;
    var currencyRepo = provider.GetRequiredService<CurrencyRepository>();
    return new FetchingService(token, currencyRepo);
});

var app = builder.Build();

app.MapGet("/health", () => "Server running.");
app.MapGet("/update", async (FetchingService updater) => await updater.UpdateNow());

app.CurrencyConvertionEndpointConfiguration();

app.Run();
