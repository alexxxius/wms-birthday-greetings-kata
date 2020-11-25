using System;
using System.Threading.Tasks;
using BirthdayGreetings.Core;
using FluentAssertions;
using Xunit;

namespace BirthdayGreetings.Tests
{
    public abstract class EmployeeCatalogTests
    {
        protected abstract IEmployeeCatalog CreateCatalogWithEmployees((String, String, String) employee1, (String, String, String) employee2);

        [Fact]
        public async Task LoadEmployees()
        {
            var employeeCatalog = CreateCatalogWithEmployees(
                ("test", "2020/10/28", "a@a.it"), 
                ("Mary", "1982/11/08", "mary.ann@foobar.com"));

            var employees = await employeeCatalog.Load();

            employees.Should()
                .BeEquivalentTo(
                    new Employee(new DateOfBirth(10, 28), new EmailInfo("test", "a@a.it")),
                    new Employee(new DateOfBirth(11, 08), new EmailInfo("Mary", "mary.ann@foobar.com"))
                );
        }

        [Fact]
        public async Task NoEmployees()
        {
            var employeeCatalog = CreateEmptyCatalog();

            var employees = await employeeCatalog.Load();

            employees.Should()
                .BeEmpty("", employees);
        }

        protected abstract IEmployeeCatalog CreateEmptyCatalog();
    }
}