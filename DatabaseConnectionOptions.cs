using System;
namespace AzureFunctions.MongoDBDriver
{
    public class DatabaseConnectionOptions
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
    }
}
