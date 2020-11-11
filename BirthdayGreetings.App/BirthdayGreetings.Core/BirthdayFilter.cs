using System;
using System.Collections.Generic;
using System.Linq;

namespace BirthdayGreetings.Core
{
    public class BirthdayFilter
    {
        readonly List<Employee> employees;

        public BirthdayFilter(List<Employee> employees) => 
            this.employees = employees;

        public List<EmailInfo> Apply(DateTime today) =>
            employees
                .Where(x => x.DateOfBirth.IsBirthday(today))
                .Select(x => x.EmailInfo)
                .ToList();
    }
}