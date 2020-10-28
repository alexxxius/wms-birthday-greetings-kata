using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BirthdayGreetings.App
{
    public class TextFileEmployeeCatalog
    {
        readonly FileConfiguration configuration;

        public TextFileEmployeeCatalog(FileConfiguration configuration) =>
            this.configuration = configuration;

        public async Task<List<Employee>> Load()
        {
            var lines = await LoadLinesOrDefault();
            return ParseEmployeeLines(lines);
        }

        async Task<String[]> LoadLinesOrDefault()
        {
            try
            {
                return await File.ReadAllLinesAsync(configuration.FilePath);
            }
            catch (FileNotFoundException)
            {
                return new string[0];
            }
        }

        static List<Employee> ParseEmployeeLines(String[] lines) =>
            lines
                .Skip(1) // NOTE: skip header
                .Select(ParseEmployeeLine)
                .ToList();

        static Employee ParseEmployeeLine(String line)
        {
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