using System;
using BirthdayGreetings.App;
using Xunit;

namespace BirthdayGreetings.Tests
{
    public class DateOfBirthTests
    {
        [Theory]
        [InlineData(2020)]
        [InlineData(2019)]
        public void YesBirthday(int year)
        {
            var dateOfBirth = new DateOfBirth(11, 8);
            var isBirthday = dateOfBirth.IsBirthday(new DateTime(year, 11, 8));
            Assert.True(isBirthday);
        }
        
        [Theory]
        [InlineData(12, 8)]
        [InlineData(11, 9)]
        public void NoBirthday(int month, int day)
        {
            var dateOfBirth = new DateOfBirth(11, 8);
            var isBirthday = dateOfBirth.IsBirthday(new DateTime(2020, month, day));
            Assert.False(isBirthday);
        }

        [Fact]
        public void BornOnLeapYearAndCheckYesBirthdayOnCommonYear()
        {
            var dateOfBirth = new DateOfBirth(2, 29);
            var isBirthday = dateOfBirth.IsBirthday(new DateTime(2019, 2, 28));
            Assert.True(isBirthday);
        }

        [Fact]
        public void BornOnLeapYearAndCheckNoBirthdayOnLeapYear()
        {
            var dateOfBirth = new DateOfBirth(2, 29);
            var isBirthday = dateOfBirth.IsBirthday(new DateTime(2020, 2, 28));
            Assert.False(isBirthday);
        }

        [Fact]
        public void BornOnLeapYearAndCheckYesBirthdayOnLeapYear()
        {
            var dateOfBirth = new DateOfBirth(2, 29);
            var isBirthday = dateOfBirth.IsBirthday(new DateTime(2020, 2, 29));
            Assert.True(isBirthday);
        }
    }
}