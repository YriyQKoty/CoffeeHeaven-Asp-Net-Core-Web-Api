using System.Collections.Generic;
using Library.Core.Concrete.Models;

namespace Library.Core.Abstract.Managers
{
    public interface IOriginCountryManager : IManager
    {
        IEnumerable<OriginCountry> GetAllCountries();
          
        OriginCountry GetCountryById(int id);
       
        OriginCountry  FindCountry (int id);
       
        OriginCountry  RemoveCountry (int id);
    }
}