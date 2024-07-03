# CurrencyConverter

## What is this?

This repository contains a small and simple .NET API that retrieves currency exchange rates from [fixer.io](https://fixer.io), saves them in a db and provides a currecy convertion service through a simple API. 

## How to run?

Before running anything all users must input a valid access token for fixer.io into the `appsettings.json` file, see the [appsettings.example.json](./CurrencyConverterAPI/appsettings.example.json) file for an example. 
This is simply to allow for the API to retrieve the data required. 


After inputting the fixer token, the simplest way to run the project is to use Docker Desktop. Navigate to the root of the project and use the command 
```
docker compose up -d
```

When this command is used the database and backend will both be downloaded, installed, and initalized. After these are setup the API is available on `localhost:5143`.

To run the CLI interface, ensure you have .NET 8 installed, then navigate to the `CurrencyConverterCLI` folder and run the dotnet project like this (from root of project): 
```
cd CurrencyConverterCLI
dotnet build
dotnet run
```

To run without docker (not recommended):
<details>

If you wish to run the project without docker, it is possible but would require some modification of code. 

In the `CurrencyConverterAPI` folder you would need to changes the connection string contains in the `Program.cs` file, that by default is 
```
builder.Services.AddDbContext<DataContext>
    (
        opt => opt.UseCosmos("https://localhost:8081", "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==", databaseName: "CosmosDb")
    );
```

Change the host and the access token.

After that you can then simply run the API on its own using the commands (from root)
```
cd CurrencyConverterAPI
dotnet build
dotnet run
```

The API would then be available on `localhost:5143`.
</details>

### API paths

The basic `localhost:5143/` path will provide access to all the currencies and their exchange rates. 

The `localhost:5143/` path also provides a POST endpoint, that takes two currency codes and amount of one of the currencies, and returns the equivalent amount of the other currency.
The post request should contains a body with a `ConvertionRequest` json:
```
{
    "CurrencyCode1": string (3-character currency code),
    "CurrencyCode2": string (3-character currency code),
    "CurrencyAmount": number,
}
```

The `localhost:5143/health` path is a status path to validate that the server is running, and provides no other useful information.

The `localhost:5143/update` path forces a fetch of the external API to update the database. 




## Implementation notes

### General design note

This implementation does not account for need to scale up significantly, and intended for only 1 instance of the API running. 
For scaling the API fetching from external source should be separated, and the user API servers could then easily scale to server more users. 


### CLI

The CLI interface is a bare-bones version, but some guard rails in terms of input validation has been utilized.

You have to know the currency codes, and they need to be on the database. In future iterations this could be checked and ability to print valid currency codes could be added.

### Database

A Cosmos DB is attempted to be used for this project, which I have never used before. The nature of the data does not offer significant benefits for relational databases, so I intended to test the NoSQL Cosmos DB.

*Both due to no-experience and (probably) trying to run it in the local emulator, the connection doesn't currently function.* 

### API

The API is a .NET 8 minimal API, with a Entity Framework Core interface to the database. These were chosen due to the limited scope of the project, allowing for quick and secure implementation of core functionality. 

The Services, Repository, Endpoints, and Models have all been separated as far as possible to both make the repository easy to understand and to allow for future expansion. 


## Time estimate

<details>


| Topic     | Estimated time| 
| -         | -         |
| Database  | 2h 30min  | 
| CLI*      | 1h        | 
| Docker    | 40min     | 
| Data processing | 40min|
| Documentation | 20min | 
| Total     |  5h 10min | 

</details>