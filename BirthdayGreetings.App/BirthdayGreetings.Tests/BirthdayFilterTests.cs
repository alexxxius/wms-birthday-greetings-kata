using System;
using System.Collections.Generic;
using BirthdayGreetings.App;
using Xunit;

namespace BirthdayGreetings.Tests
{
    public class BirthdayFilterTests
    {
        [Fact]
        public void NoEmployee()
        {
            var employees = new List<Employee>();
            var result = new BirthdayFilter(employees)
                .Apply(DateTime.Today);
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