using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BirthdayGreetings.App;
using BirthdayGreetings.Core;
using FluentAssertions.Execution;
using Moq;
using Xunit;

namespace BirthdayGreetings.Tests
{
    public class DefaultBirthdayServiceTests
    {
        [Fact]
        public void NoSendsGreetingWhenNoBirthdays()
        {
            var employeeCatalog = new StubNoEmployeesCatalog();
            var greetingsNotification = new SpyGreetingsNotificationHistory();
            var service = new DefaultBirthdayService(employeeCatalog, greetingsNotification);

            service.SendGreetings(new DateTime(2020, 11, 25));

            Assert.Empty(greetingsNotification.EmailSent);
        }
        
        [Fact]
        public void NoSendsGreetingWhenNoBirthdays_InMem()
        {
            var employeeCatalog = new InMemoryEmployeeCatalog();
            var greetingsNotification = new InMemoryGreetingsNotification();
            var service = new DefaultBirthdayService(employeeCatalog, greetingsNotification);

            service.SendGreetings(new DateTime(2020, 11, 25));

            Assert.Empty(greetingsNotification.EmailSent);
        }
        
        [Fact]
        public void NoSendsGreetingWhenNoBirthdays_()
        {
            var employeeCatalog = new StubNoEmployeesCatalog();
            var greetingsNotification = new MockGreetingsNotigication();
            var service = new DefaultBirthdayService(employeeCatalog, greetingsNotification);

            greetingsNotification
                .ExpectSendBirthdayCalledEmptyList();
            service.SendGreetings(new DateTime(2020, 11, 25));

            greetingsNotification.Verify();
        }
        
        [Fact]
        public void NoSendsGreetingWhenNoBirthdays_Moq()
        {
            // Mock<> == Test Double Factory
            var employeeCatalog = new Mock<IEmployeeCatalog>();
            var greetingsNotification = new Mock<IGreetingsNotification>();
            var service = new DefaultBirthdayService(employeeCatalog.Object, greetingsNotification.Object);

            // stub - canned result - query
            employeeCatalog.Setup(x => x.Load()).Returns(Task.FromResult(new List<Employee>()));
            service.SendGreetings(new DateTime(2020, 11, 25));

            // mock - verify expectation - command
            greetingsNotification.Verify(x => x.SendBirthday(new List<EmailInfo>()));
        }
        
        [Fact(Skip = "demo")]
        public void NoSendsGreetingWhenNoBirthdays_Moq_Drift()
        {
            // Mock<> == Test Double Factory
            var employeeCatalog = new Mock<IEmployeeCatalog>();
            var greetingsNotification = new Mock<IGreetingsNotification>();
            var service = new DefaultBirthdayService(employeeCatalog.Object, greetingsNotification.Object);

            // stub - canned result - query
            employeeCatalog.Setup(x => x.Load()).Returns(Task.FromResult<List<Employee>>(null));
            service.SendGreetings(new DateTime(2020, 11, 25));

            // mock - verify expectation - command
            greetingsNotification.Verify(x => x.SendBirthday(new List<EmailInfo>()));
        }

        public class MockGreetingsNotigication : IGreetingsNotification
        {
            Int32 expectedCalledCount = 0;
            Int32 calledCount = 0;
            Boolean expectEmptyList = false;
            Boolean actualEmptyList = false;

            public Task SendBirthday(IList<EmailInfo> infos)
            {
                calledCount += 1;
                actualEmptyList = infos.Count == 0;
                return Task.CompletedTask;
            }

            public void Verify()
            {
                if (actualEmptyList != expectEmptyList)
                    throw new AssertionFailedException("different called count");
                if (calledCount != expectedCalledCount)
                    throw new AssertionFailedException("different called count");
            }

            public void ExpectSendBirthdayCalledEmptyList()
            {
                expectedCalledCount = 1;
                expectEmptyList = true;
            }
        }

        public class SpyGreetingsNotificationHistory : IGreetingsNotification
        {
            public SpyGreetingsNotificationHistory()
            {
                EmailSent = new List<EmailInfo>();
            }

            public List<EmailInfo> EmailSent { get; set; }

            public Task SendBirthday(IList<EmailInfo> infos)
            {
                EmailSent.AddRange(infos);
                return Task.CompletedTask;
            }
        }

        public class StubNoEmployeesCatalog : IEmployeeCatalog
        {
            public Task<List<Employee>> Load()
            {
                return Task.FromResult(new List<Employee>());
            }

            public Task<List<Employee>> LoadBy(DateOfBirth dateOfBirth)
            {
                throw new NotImplementedException();
            }
        }

        public class StubEmployeeCatalog : IEmployeeCatalog
        {
            List<Employee> employees;

            public Task<List<Employee>> Load()
            {
                return Task.FromResult(employees);
            }

            public Task<List<Employee>> LoadBy(DateOfBirth dateOfBirth)
            {
                return Task.FromResult(employees);
            }

            public void OnAnyLoadReturns(List<Employee> employees)
            {
                this.employees = employees;
            }
        }
    }
}