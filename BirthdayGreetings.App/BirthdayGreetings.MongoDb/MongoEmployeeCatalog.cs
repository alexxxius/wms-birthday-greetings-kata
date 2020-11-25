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

        public Task<List<Employee>> LoadBy(DateOfBirth dateOfBirth)
        {
            // TODO: create filter query by born date
            throw new NotImplementedException();
        }
    }
}