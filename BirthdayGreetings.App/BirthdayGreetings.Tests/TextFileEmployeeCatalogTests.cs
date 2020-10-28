using System.Threading.Tasks;
using BirthdayGreetings.App;
using FluentAssertions;
using Xunit;
using Xunit.Sdk;
using static BirthdayGreetings.Tests.Support.EmployeeFile;

namespace BirthdayGreetings.Tests
{
    public class TextFileEmployeeCatalogTests
    {
        readonly FileConfiguration fileConfiguration = new FileConfiguration
        {
            FilePath = "employees.txt"
        };

        [Fact]
        public async Task LoadEmployees()
        {
            File(fileConfiguration.FilePath,
                Header(),
                Employee("test", "2020/10/28", "a@a.it"),
                Employee("Mary", "1982/11/08", "mary.ann@foobar.com"));
            var employeeCatalog = new TextFileEmployeeCatalog(fileConfiguration);

            var employees = await employeeCatalog.Load();

            employees.Should()
                .BeEquivalentTo(
                    new Employee(new DateOfBirth(10, 28), new EmailInfo("test", "a@a.it")),
                    new Employee(new DateOfBirth(11, 08), new EmailInfo("Mary", "mary.ann@foobar.com"))
                );
        }
        
        [Fact]
        public async Task MissingFile()
        {
            DeleteFile(fileConfiguration.FilePath);
            var employeeCatalog = new TextFileEmployeeCatalog(fileConfiguration);

            var employees = await employeeCatalog.Load();

            employees.Should()
                .BeEmpty("", employees);
        }
        
        /*
         * TODO:
- file no-head yes rows => no sends
- file full empty => no sends
- righe malformattate
- righe duplicate
         * 
         */
    }
}