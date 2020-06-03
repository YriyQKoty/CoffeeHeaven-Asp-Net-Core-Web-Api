using System.Collections.Generic;
using Library.Core.Abstract.Managers;
using Library.Core.Abstract.Repositories;
using Library.Core.Concrete.Models;

namespace Library.Core.Concrete.Managers
{
    public class OriginCountryManager : IOriginCountryManager
    {
        private readonly IOriginCountryRepository _originCountryRepository;

        public OriginCountryManager(IOriginCountryRepository originCountryRepository)
        {
            _originCountryRepository = originCountryRepository;
        }

        public IEnumerable<OriginCountry> GetAllCountries()
        {
            return _originCountryRepository.GetAll();
        }
        public OriginCountry GetCountryById(int id)
        {
            return _originCountryRepository.Get(id);
        }
        
        public OriginCountry FindCountry(int id)
        {
            return _originCountryRepository.SingleOrDefault(country => country.Id == id);
        }

        public IEnumerable<OriginCountry> GetCountriesWithProviders()
        {
            return _originCountryRepository.GetCountriesWithProviders();
        }


        public OriginCountry GetCountryWithProviders(int id)
        {
            return _originCountryRepository.GetCountryWithProviders(id);
        }

        public OriginCountry RemoveCountry(int id)
        {
            return _originCountryRepository.SingleOrDefault(country => country.Id == id);
        }
        
        public int SaveChanges()
        {
            return _originCountryRepository.SaveChanges(); 
        }

        public void Add(OriginCountry originCountry)
        {
            _originCountryRepository.Add(originCountry);
        }

        public void Remove(OriginCountry originCountry)
        {
           _originCountryRepository.Remove(originCountry);
        }
    }
}