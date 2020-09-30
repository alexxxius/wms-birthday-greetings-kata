using System;
using System.Collections.Specialized;

namespace BirthdayGreetings.App
{
    public class FileConfiguration
    {
        public String FilePath { get; set; }

        public static FileConfiguration From(NameValueCollection appSettings)
        {
            throw new NotImplementedException();
        }
    }
}