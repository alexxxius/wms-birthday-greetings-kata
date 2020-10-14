using System;

namespace BirthdayGreetings.App
{
    public class DateOfBirth
    {
        readonly Int32 month;
        readonly Int32 day;
        readonly Boolean isBornOnLeapYear;

        public DateOfBirth(in int month, in int day)
        {
            this.month = month;
            this.day = day;
            isBornOnLeapYear = this.month == 2 && this.day == 29;
        }

        public bool IsBirthday(DateTime today) =>
            isBornOnLeapYear && IsCommonYear(today) 
                ? CheckLeapCase(today) 
                : CheckCommonCase(today);

        static Boolean IsCommonYear(DateTime today) => 
            !DateTime.IsLeapYear(today.Year);

        static Boolean CheckLeapCase(DateTime today) => 
            today.Month == 2 && today.Day == 28;

        Boolean CheckCommonCase(DateTime today) => 
            today.Month == month && today.Day == day;

        public static DateOfBirth From(DateTime dateOfBirth) => 
            new DateOfBirth(dateOfBirth.Month, dateOfBirth.Day);

        public static DateOfBirth From(String value) => 
            From(DateTime.Parse(value));
    }
}