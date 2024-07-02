using CurrencyConverterAPI.Services;

var builder = WebApplication.CreateBuilder(args);
FetchingService updater = new FetchingService(builder.Configuration.GetConnectionString("fixerToken")!);


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

await updater.UpdateNow();

app.Run();
