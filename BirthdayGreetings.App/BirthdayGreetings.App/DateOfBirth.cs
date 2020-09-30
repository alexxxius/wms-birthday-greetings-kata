using System;

namespace BirthdayGreetings.App
{
    public class DateOfBirth
    {
        readonly Int32 month;
        readonly Int32 day;

        public DateOfBirth(in int month, in int day)
        {
            this.month = month;
            this.day = day;
        }

        public bool IsBirthday(DateTime today) => 
            today.Month == month && today.Day == day;

        public static DateOfBirth From(DateTime dateOfBirth) => 
            new DateOfBirth(dateOfBirth.Month, dateOfBirth.Day);

        public static DateOfBirth From(String value) => 
            From(DateTime.Parse(value));
    }
}