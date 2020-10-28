using System;

namespace BirthdayGreetings.Tests.Support
{
    public static class EmployeeFile
    {
        public static void DeleteFile(string fileName) =>
            System.IO.File.Delete(fileName);
        public static void File(string fileName, params string[] lines) =>
            System.IO.File.WriteAllLines(fileName, lines);

        public static String Employee(String name, String date, String email) =>
            $"Ann, {name}, {date}, {email}";

        public static String Header() =>
            "last_name, first_name, date_of_birth, email";
    }
}