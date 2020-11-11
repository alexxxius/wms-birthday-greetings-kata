using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BirthdayGreetings.Core;

namespace BirthdayGreetings.FileSystem
{
    // Adapter in Port/Adapter architecture
    public class TextFileEmployeeCatalog : IEmployeeCatalog
    {
        readonly FileConfiguration configuration;

        public TextFileEmployeeCatalog(FileConfiguration configuration) =>
            this.configuration = configuration;

        public async Task<List<Employee>> Load()
        {
            var lines = await LoadLinesOrDefault();
            return EmployeeFileParser.ParseLines(lines);
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
    }
}