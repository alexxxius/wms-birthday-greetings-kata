using System;
using BirthdayGreetings.Core;

namespace BirthdayGreetings.Tests
{
    public class MongoDbEmployeeCatalogTests : EmployeeCatalogTests
    {
        protected override IEmployeeCatalog CreateCatalogWithEmployees((String, String, String) employee1, (String, String, String) employee2)
        {
            throw new NotImplementedException();
        }

        protected override IEmployeeCatalog CreateEmptyCatalog()
        {
            throw new NotImplementedException();
        }
    }
}