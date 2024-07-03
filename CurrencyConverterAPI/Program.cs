using CurrencyConverterAPI.Data;
using CurrencyConverterAPI.Repository;
using CurrencyConverterAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
FetchingService updater = new FetchingService(builder.Configuration.GetConnectionString("fixerToken")!);

builder.Services.AddDbContext<DataContext>
    (
        opt => opt.UseCosmos("https://localhost:8081", "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==", databaseName: "currencyDb")
    );

builder.Services.AddScoped<CurrencyRepository, CurrencyRepository>();

var app = builder.Build();
app.UseHttpsRedirection();

app.MapGet("/", () => "Hello World!");

app.MapGet("/update", async () => await updater.UpdateNow());

app.Run();
