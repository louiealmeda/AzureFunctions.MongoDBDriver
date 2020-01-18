using System;
using MongoDB.Driver;

namespace AzureFunctions.MongoDBDriver
{
    public interface IMongoDBClient
    {
        MongoClient Client { get; set; }
        IMongoDatabase Database { get; set; }
    }
}
