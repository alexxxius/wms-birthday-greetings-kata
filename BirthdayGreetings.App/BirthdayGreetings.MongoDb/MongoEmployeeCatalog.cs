using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BirthdayGreetings.Core;
using MongoDB.Driver;

namespace BirthdayGreetings.MongoDb
{
    public class MongoEmployeeCatalog : IEmployeeCatalog
    {
        readonly IMongoDatabase database;

        public MongoEmployeeCatalog(IMongoDatabase database)
        {
            this.database = database;
        }

        public Task<List<Employee>> Load()
        {
            throw new NotImplementedException();
        }
    }
}