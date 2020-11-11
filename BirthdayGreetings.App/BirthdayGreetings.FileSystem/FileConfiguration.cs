using System;
using System.Collections.Specialized;

namespace BirthdayGreetings.FileSystem
{
    public class FileConfiguration
    {
        public String FilePath { get; set; }

        public static FileConfiguration From(NameValueCollection appSettings)
        {
            return new FileConfiguration();
        }
    }
}