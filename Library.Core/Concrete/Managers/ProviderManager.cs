using System.Collections.Generic;
using Library.Core.Abstract.Managers;
using Library.Core.Abstract.Repositories;
using Library.Core.Concrete.Models;

namespace Library.Core.Concrete.Managers
{
    public class ProviderManager : IProviderManager
    {
        private readonly IProviderRepository _providerRepository;

        public ProviderManager(IProviderRepository providerRepository)
        {
            _providerRepository = providerRepository;
        }

        public IEnumerable<Provider> GetAllProviders()
        {
            return _providerRepository.GetAll();
        }
        public Provider GetProviderById(int id)
        {
            return _providerRepository.Get(id);
        }
        
        public Provider FindProvider(int id)
        {
            return _providerRepository.SingleOrDefault(provider => provider.Id == id);
        }
        
        public Provider RemoveProvider(int id)
        {
            return _providerRepository.SingleOrDefault(provider => provider.Id == id);
        }
      
        public int SaveChanges()
        {
            return _providerRepository.SaveChanges(); 
        }
        
    }
}