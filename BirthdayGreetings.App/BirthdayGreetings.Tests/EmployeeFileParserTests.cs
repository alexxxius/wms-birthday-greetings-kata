using System;
using BirthdayGreetings.App;
using FluentAssertions;
using Xunit;
using static BirthdayGreetings.Tests.Support.EmployeeFile;

namespace BirthdayGreetings.Tests
{
    public class EmployeeFileParserTests
    {
        public class SingleLine
        {
            [Fact]
            public void ValidLine()
            {
                var line = Employee("test", "2020/10/28", "a@a.it");
                var employee = EmployeeFileParser.ParseLine(line);
                employee.Should()
                    .BeEquivalentTo(new Employee(new DateOfBirth(10, 28), new EmailInfo("test", "a@a.it")));
            }

            [Fact]
            public void EmptyLine()
            {
                var line = String.Empty;
                Assert.Throws<InvalidOperationException>(() => EmployeeFileParser.ParseLine(line));
            }

            /*
             * TODO:
                - riga malformattate
                - riga missing column
             */
        }

        public class ManyLines
        {
            /*
             * TODO:
                - no-head yes rows
                - yes-header but empty
                - righe duplicate
             */
        }
    }
}