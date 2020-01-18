using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AzureFunctions.MongoDBDriver
{
    public class MongoDBClient : IMongoDBClient
    {
        public MongoClient Client { get; set; }
        public IMongoDatabase Database { get; set; }

        public MongoDBClient(IOptions<DatabaseConnectionOptions> options)
        {
            Console.WriteLine("Connecting to database");

            Client = new MongoClient(options.Value.ConnectionString);
            Database = Client.GetDatabase(options.Value.Database);

        }



    }
}
