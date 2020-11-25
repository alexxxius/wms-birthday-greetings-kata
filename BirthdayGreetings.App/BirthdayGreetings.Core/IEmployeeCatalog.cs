using System.Collections.Generic;
using System.Threading.Tasks;

namespace BirthdayGreetings.Core
{
    // Port (output/secondary) in Port/Adapter architecture
    public interface IEmployeeCatalog
    {
        Task<List<Employee>> Load(); // null || Task<List<Employee>> || Exception
        Task<List<Employee>> LoadBy(DateOfBirth dateOfBirth);
    }
}