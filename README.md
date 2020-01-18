# AzureFunctions.MongoDBDriver
MongoDB Driver wrapper for easy Azure Functions Dependency Injection running in Singleton for optimum Function performance



## Why

Azure functions are designed to be quick and complete fast. The longer a function executes, the higher you pay (if you're in a consumption plan.) 

Establishing a connection to mongodb on every function call is bad because:  
1. Establishing a connection is slow (slower response time for each request)
2. Functions may easily max out the maximum connections mongodb is willing to accept

[Azure Function Recommendation on managing connections](https://docs.microsoft.com/en-us/azure/azure-functions/manage-connections)

This library simplifies your setup so you won't have to worry connecting to your database and making sure it reuses a single connection.

## Usage

```shell
Install-Package AzureFunctions.MongoDBDriver -Version 1.0.1
```

or search for `AzureFunctions.MongoDBDriver`



### 1. Register the service in your Startup.cs
```csharp
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using AzureFunctions.MongoDBDriver;

[assembly: FunctionsStartup(typeof(MyNamespace.Startup))]

namespace MyNamespace
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            //... 
            //your other registrations here
            
            builder.Services.AddMongoDBClient();
        }
    }

```
> Create a Startup.cs if you don't have one.

### 2. Add your connection strings in the local.settings.json


```json
{

  "Values": {

    "ConnectionString": "MyConnectionStringWithCredentials",
    "Database":  "MyDatabaseName"
  }
}
```

### 3. Inject and use it in your function

Inject the client in the constructor. Save it to a static variable, and use it normally.

```csharp
namespace MyNamespace
{
    public class MyFunctionName
    {

        private static IMongoDatabase db;

        public Courses(IMongoDBClient client)
        {
            db = client.Database;
        }

        [FunctionName("TestClient")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "courses")] HttpRequest req,
            ILogger log
            )
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            
            //Use mongodb driver as you would normally do
            var collection = db.GetCollection<BsonDocument>("MyCollectionName");

            List<BsonDocument> ret = collection.FindSync("{}").ToList();

            return new OkObjectResult(ret);

        }
    }
}
```
