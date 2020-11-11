using System.Collections.Generic;
using System.Threading.Tasks;

namespace BirthdayGreetings.Core
{
    // Port (output/secondary) in Port/Adapter architecture
    public interface IGreetingsNotification
    {
        Task SendBirthday(IList<EmailInfo> infos);
    }
}