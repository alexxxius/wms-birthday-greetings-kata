using System;
using System.Threading.Tasks;

namespace BirthdayGreetings.Core
{
    // Port (input/primary)
    public interface IBirthdayService
    {
        Task SendGreetings(DateTime today);
    }
}