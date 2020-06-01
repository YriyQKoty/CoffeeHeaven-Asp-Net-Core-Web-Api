using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Library.Core.Abstract.Repositories;
using Library.Core.Concrete.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Core.Concrete.Repositories
{
    public class ProviderRepository : Repository<Provider>, IProviderRepository
    {
        public ProviderRepository(DbContext context) : base(context)
        {
        
        }
        
        public Provider GetProviderWithCoffees(int id)
        {
            return ApplicationDbContext.Providers.Include(c => c.Coffees).SingleOrDefault(p => p.Id == id);
        }

        public IEnumerable<Provider> GetProvidersWithCoffees()
        {
            return ApplicationDbContext.Providers.Include(c => c.Coffees).ToList();
        }
    }
}