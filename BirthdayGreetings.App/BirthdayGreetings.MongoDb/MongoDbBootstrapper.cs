using System;
using MongoDB.Driver;

namespace BirthdayGreetings.MongoDb
{
    public static class MongoDbBootstrapper
    {
        public static IMongoDatabase Init(String connectionString)
        {
            var client = new MongoClient(connectionString);
            return client.GetDatabase("employees");
        }
    }
}