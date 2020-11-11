using System;
using System.Collections.Generic;
using System.Linq;
using BirthdayGreetings.Core;

namespace BirthdayGreetings.FileSystem
{
    public static class EmployeeFileParser
    {
        public static List<Employee> ParseLines(String[] lines) =>
            lines
                .Skip(1) // NOTE: skip header
                .Select(ParseLine)
                .ToList();

        public static Employee ParseLine(String line)
        {
            if (String.IsNullOrWhiteSpace(line))
                throw new InvalidOperationException();
            
            var parts = SplitLine(line);
            return new Employee(
                DateOfBirth.From(parts[2]),
                new EmailInfo(parts[1], parts[3])
            );
        }

        static String[] SplitLine(String line) =>
            line
                .Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToArray();
    }
}