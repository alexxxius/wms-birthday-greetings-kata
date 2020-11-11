using System;
using System.Collections.Specialized;
using Microsoft.Extensions.Configuration;

namespace BirthdayGreetings.FileSystem
{
    public class FileConfiguration
    {
        public String FilePath { get; set; }

        public static FileConfiguration From(NameValueCollection appSettings)
        {
            throw new NotImplementedException();
        }

        public static FileConfiguration From(IConfiguration configuration)
        {
            throw new NotImplementedException();
        }
    }
}