using System;
using System.Collections.Generic;
using BirthdayGreetings.App;
using BirthdayGreetings.Core;
using Xunit;

namespace BirthdayGreetings.Tests
{
    public class BirthdayFilterTests
    {
        [Fact]
        public void NoEmployees()
        {
            var employees = new List<Employee>();
            var result = new BirthdayFilter(employees)
                .Apply(new DateTime(2020, 11, 10));
            Assert.Empty(result);
        }
        
        /*
         * TODO:
         * - one employee, one party => one email
         * - many employee, one party => one email
         * - many employee, many party => many email
         */
    }
}