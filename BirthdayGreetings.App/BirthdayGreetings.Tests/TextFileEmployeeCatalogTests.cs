using System;
using System.Threading.Tasks;
using BirthdayGreetings.Core;
using BirthdayGreetings.FileSystem;
using FluentAssertions;
using Xunit;
using static BirthdayGreetings.Tests.Support.EmployeeFile;

namespace BirthdayGreetings.Tests
{
    public class TextFileEmployeeCatalogTests : EmployeeCatalogTests
    {
        readonly FileConfiguration fileConfiguration = new FileConfiguration
        {
            FilePath = "employees.txt"
        };

        protected override IEmployeeCatalog CreateCatalogWithEmployees((String, String, String) employee1, (String, String, String) employee2)
        {
            File(fileConfiguration.FilePath,
                Header(),
                Employee(employee1),
                Employee(employee2));
            return new TextFileEmployeeCatalog(fileConfiguration);
        }

        protected override IEmployeeCatalog CreateEmptyCatalog()
        {
            File(fileConfiguration.FilePath,
                Header());
            return new TextFileEmployeeCatalog(fileConfiguration);
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
    }
}