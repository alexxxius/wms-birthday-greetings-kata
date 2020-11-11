using System;
using System.Threading.Tasks;

namespace BirthdayGreetings.Core
{
    // Application Service (sembra un adapter perche' implementa una porta,
    // ma non e' un adapter perche' sta nel dominio)
    public class DefaultBirthdayService : IBirthdayService
    {
        readonly IEmployeeCatalog employeeCatalog;
        readonly IGreetingsNotification smtpGreetingsNotification;

        public DefaultBirthdayService(IEmployeeCatalog employeeCatalog, IGreetingsNotification smtpGreetingsNotification)
        {
            this.employeeCatalog = employeeCatalog;
            this.smtpGreetingsNotification = smtpGreetingsNotification;
        }

        public async Task SendGreetings(DateTime today)
        {
            var allEmployees = await employeeCatalog.Load();
            var birthdayEmployees =
                new BirthdayFilter(allEmployees)
                    .Apply(today);
            await smtpGreetingsNotification.SendBirthday(birthdayEmployees);
        }
    }
}