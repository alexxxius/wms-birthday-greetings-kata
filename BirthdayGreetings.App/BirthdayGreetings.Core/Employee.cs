namespace BirthdayGreetings.Core
{
    public class Employee
    {
        public DateOfBirth DateOfBirth { get; }
        public EmailInfo EmailInfo { get; }

        public Employee(DateOfBirth dateOfBirth, EmailInfo emailInfo)
        {
            DateOfBirth = dateOfBirth;
            EmailInfo = emailInfo;
        }
    }
}