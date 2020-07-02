using System;
using System.Collections.Generic;
using AutoMapper;
using Library.Core.Abstract.Managers;
using Library.Core.Abstract.Repositories;
using Library.Core.Concrete.Models;


namespace Library.Core.Concrete.Managers
{
    public class CoffeeManager : ICoffeeManager
    {
        private readonly ICoffeeRepository _coffeeRepository;
        private readonly IProviderRepository _providerRepository;
        

        public CoffeeManager(ICoffeeRepository coffeeRepository, IProviderRepository providerRepository)
        {
            _coffeeRepository = coffeeRepository;
            _providerRepository = providerRepository;
        }

        public IEnumerable<Coffee> GetAllCoffees()
        {
            return _coffeeRepository.GetAll();
        }

        public Coffee GetCoffee(int id)
        {
            return _coffeeRepository.Get(id);
        }
        
        public void Remove(Coffee coffee)
        {
            _coffeeRepository.Remove(coffee);
        }
        

        public int SaveChanges()
        {
            return _coffeeRepository.SaveChanges();
        }


        public void Add(Coffee coffee)
        {
            _coffeeRepository.Add(coffee);
        }
        
        public bool DoesProviderIdExist(Coffee coffee)
        {
            return  _providerRepository.SingleOrDefault(p => p.Id == coffee.ProviderId) != null;
        }
    }
}