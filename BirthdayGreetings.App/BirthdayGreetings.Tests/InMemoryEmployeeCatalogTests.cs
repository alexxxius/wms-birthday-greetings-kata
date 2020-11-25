using System;
using BirthdayGreetings.App;
using BirthdayGreetings.Core;

namespace BirthdayGreetings.Tests
{
    public class InMemoryEmployeeCatalogTests : EmployeeCatalogTests
    {
        static Employee Employee((String name, String date, String email) data)
        {
            var (name, date, email) = data;
            return Employee(name, date, email);
        }

        static Employee Employee(string name, string dateOfBirth, string email) =>
            new Employee(DateOfBirth.From(dateOfBirth),new EmailInfo(name, email));

        protected override IEmployeeCatalog CreateCatalogWithEmployees((String, String, String) employee1, (String, String, String) employee2) =>
            new InMemoryEmployeeCatalog(
                Employee(employee1),
                Employee(employee2));

        protected override IEmployeeCatalog CreateEmptyCatalog() => 
            new InMemoryEmployeeCatalog();
    }
}