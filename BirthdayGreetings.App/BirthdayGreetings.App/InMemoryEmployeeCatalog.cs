using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BirthdayGreetings.Core;

namespace BirthdayGreetings.App
{
    public class InMemoryEmployeeCatalog : IEmployeeCatalog
    {
        readonly Employee[] es;
        
        public InMemoryEmployeeCatalog()
            :this(new Employee[0])
        {
        }

        public InMemoryEmployeeCatalog(params Employee[] es)
        {
            this.es = es;
        }

        public Task<List<Employee>> Load() =>
            Task.Delay(10)
                .ContinueWith(_ => es.ToList());

        public Task<List<Employee>> LoadBy(DateOfBirth dateOfBirth) =>
            Task.Delay(10)
                .ContinueWith(_ => es.Where(x => x.DateOfBirth == dateOfBirth).ToList());
    }
}