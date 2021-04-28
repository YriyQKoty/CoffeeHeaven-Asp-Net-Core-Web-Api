using System.Collections.Generic;

using System.Linq;
using Library.Api.Requests;
using Library.Core.Abstract.Managers;
using Library.Core.Abstract.Repositories;
using Library.Core.Concrete.Models;
using Microsoft.AspNetCore.Identity;

namespace Library.Api.Fakes
{
    public class FakeProviderManager : IProviderManager
    {
        public FakeProviderManager(IProviderRepository repository)
        {
        }
        private List<Provider> _providers = new List<Provider>()
        {
            new Provider() {Id = 1, OriginCountryId = 1, Coffees = new List<Coffee>()
            {
                new Coffee()
            }},
            new Provider(){Id = 2, OriginCountryId = 1},
            
        };
        private List<OriginCountry> _countries = new List<OriginCountry>()
        {
            new OriginCountry() {Id = 1}
        };




        public int SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Provider> GetAllProviders()
        {
            return _providers;
        }

        public IEnumerable<Provider> GetAllProvidersWithCoffees()
        {
            return _providers.FindAll(p => p.Coffees != null);
        }

        public Provider GetProviderWithCoffees(int id)
        {
            return _providers.Find(p => p.Id == id && p.Coffees != null);
        }

        public Provider GetProviderById(int id)
        {
            return _providers.Find(p => p.Id == id);
        }

        public Provider FindProvider(int id)
        {
            return GetProviderById(id);
        }

        public void RemoveProvider(int id)
        {
            _providers.Remove(GetProviderById(id));
        }

        public void Add(Provider provider)
        {
           _providers.Add(provider);
        }

        public void Remove(Provider provider)
        {
            _providers.Remove(provider);
        }

        public bool DoesCountryIdExist(int countryId)
        {
            return _providers?.SingleOrDefault(p => p.OriginCountryId == countryId) != null;
        }
    }
}