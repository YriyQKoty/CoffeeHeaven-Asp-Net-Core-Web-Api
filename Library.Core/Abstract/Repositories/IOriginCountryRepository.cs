using System.Collections.Generic;
using Library.Core.Concrete.Models;

namespace Library.Core.Abstract.Repositories
{
    public interface IOriginCountryRepository :IRepository<OriginCountry>
    {
        OriginCountry GetCountryWithProviders(int id);
        
        IEnumerable<OriginCountry> GetCountriesWithProviders();

    }
    
    
}