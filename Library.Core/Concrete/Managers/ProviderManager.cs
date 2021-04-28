using System.Collections.Generic;
using Library.Core.Abstract.Managers;
using Library.Core.Abstract.Repositories;
using Library.Core.Concrete.Models;

namespace Library.Core.Concrete.Managers
{
    public class ProviderManager : IProviderManager
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IOriginCountryRepository _originCountryRepository;
        public ProviderManager(IProviderRepository providerRepository, IOriginCountryRepository originCountryRepository)
        {
            _providerRepository = providerRepository;
            _originCountryRepository = originCountryRepository;
        }

        public IEnumerable<Provider> GetAllProviders()
        {
            return _providerRepository.GetAll();
        }
        
        public IEnumerable<Provider> GetAllProvidersWithCoffees()
        {
            return _providerRepository.GetProvidersWithCoffees();
        }
        

        public Provider GetProviderById(int id)
        {
            return _providerRepository.Get(id);
        }
        
        public Provider GetProviderWithCoffees(int id)
        {
            return _providerRepository.GetProviderWithCoffees(id);
        }
        public Provider FindProvider(int id)
        {
            return _providerRepository.SingleOrDefault(provider => provider.Id == id);
        }
        
        public void RemoveProvider(int id)
        {
            var provider = _providerRepository.SingleOrDefault(provider => provider.Id == id);
            _providerRepository.Remove(provider);
        }
      
        public int SaveChanges()
        {
            return _providerRepository.SaveChanges(); 
        }
        
        public void Add(Provider provider)
        {
            _providerRepository.Add(provider);
        }

        public void Remove(Provider provider)
        {
            _providerRepository.Remove(provider);
        }
        
        public bool DoesCountryIdExist(int countryId)
        {
            return  _originCountryRepository?.SingleOrDefault(oc => oc.Id == countryId) != null;
        }
        
    }
}