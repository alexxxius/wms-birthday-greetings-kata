using System;

namespace BirthdayGreetings.App
{
    public sealed class DateOfBirth
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

        public override string ToString() =>
            $"{nameof(month)}: {month}, {nameof(day)}: {day}";

        bool Equals(DateOfBirth other) => month == other.month && day == other.day;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(DateOfBirth)) return false;
            return Equals((DateOfBirth) obj);
        }

        public override int GetHashCode() => HashCode.Combine(month, day);
        public static bool operator ==(DateOfBirth left, DateOfBirth right) => Equals(left, right);
        public static bool operator !=(DateOfBirth left, DateOfBirth right) => !Equals(left, right);

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