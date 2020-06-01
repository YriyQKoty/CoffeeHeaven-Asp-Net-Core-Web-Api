using System.Collections.Generic;
using System.Linq;
using Library.Core.Abstract.Repositories;
using Library.Core.Concrete.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Core.Concrete.Repositories
{
    public class OriginCountryRepository : Repository<OriginCountry>, IOriginCountryRepository
    {
        public OriginCountryRepository(DbContext context) : base(context)
        {
        }
        
        public OriginCountry GetCountryWithProviders(int id)
        {
            return ApplicationDbContext.Countries.Include(o => o.Providers).SingleOrDefault(oc => oc.Id == id);
        }
        
        public IEnumerable<OriginCountry> GetCountriesWithProviders()
        {
            return ApplicationDbContext.Countries.Include(o => o.Providers).ToList();
        }
    }
}