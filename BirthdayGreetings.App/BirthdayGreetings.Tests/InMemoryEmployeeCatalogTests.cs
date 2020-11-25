using System;
using System.Threading.Tasks;
using BirthdayGreetings.App;
using BirthdayGreetings.Core;
using BirthdayGreetings.FileSystem;
using FluentAssertions;
using Xunit;

namespace BirthdayGreetings.Tests
{
    public class InMemoryEmployeeCatalogTests
    {
        [Fact]
        public async Task LoadEmployees()
        {
            var employeeCatalog = new InMemoryEmployeeCatalog(
                Employee("test", "2020/10/28", "a@a.it"),
                Employee("Mary", "1982/11/08", "mary.ann@foobar.com")
                );

            var employees = await employeeCatalog.Load();

            employees.Should()
                .BeEquivalentTo(
                    new Employee(new DateOfBirth(10, 28), new EmailInfo("test", "a@a.it")),
                    new Employee(new DateOfBirth(11, 08), new EmailInfo("Mary", "mary.ann@foobar.com"))
                );
        }
        
        [Fact]
        public async Task LoadEmptyEmployees()
        {
            var employeeCatalog = new InMemoryEmployeeCatalog();

            var employees = await employeeCatalog.Load();

            employees.Should()
                .BeEmpty();
        }
        
        [Fact]
        public async Task LoadEmptyEmployees_Drift()
        {
            var employeeCatalog = new InMemoryEmployeeCatalog();

            var employees = await employeeCatalog.Load();

            employees.Should()
                .BeNull();
        }

        Employee Employee(string name, string dateOfBirth, string email) =>
            new Employee(DateOfBirth.From(dateOfBirth),new EmailInfo(name, email));
    }
}