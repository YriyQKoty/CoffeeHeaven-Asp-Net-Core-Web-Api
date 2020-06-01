using System.Collections.Generic;
using Library.Core.Concrete.Models;

namespace Library.Core.Abstract.Managers
{
    public interface IProviderManager : IManager
    {
          IEnumerable<Provider> GetAllProviders();
          
          Provider GetProviderById(int id);
       
          Provider  FindProvider (int id);
       
          Provider  RemoveProvider (int id);
    }
}