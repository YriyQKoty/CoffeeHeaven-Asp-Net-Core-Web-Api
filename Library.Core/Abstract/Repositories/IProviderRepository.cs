using System.Collections.Generic;
using Library.Core.Concrete.Models;

namespace Library.Core.Abstract.Repositories
{
    public interface IProviderRepository : IRepository<Provider>
    {
        Provider GetProviderWithCoffees(int id);

        IEnumerable<Provider> GetProvidersWithCoffees();
    }
}