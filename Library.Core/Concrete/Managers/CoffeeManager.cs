using System;
using System.Collections.Generic;
using Library.Core.Abstract.Managers;
using Library.Core.Abstract.Repositories;
using Library.Core.Concrete.Models;
using Library.Core.Concrete.Repositories;
using Library.Core.Persistence;
using Microsoft.EntityFrameworkCore;

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

        public Coffee ChangeCoffee(int id)
        {
            return _coffeeRepository.SingleOrDefault(coffee => coffee.Id == id);
        }

        public Coffee RemoveCoffee(int id)
        {
            return _coffeeRepository.SingleOrDefault(coffee => coffee.Id == id);
        }

        public int SaveChanges()
        {
            return _coffeeRepository.SaveChanges();
        }


        public void Add(Coffee coffee)
        {
            _coffeeRepository.Add(coffee);
        }

        public void Remove(Coffee coffee)
        {
            _coffeeRepository.Remove(coffee);
        }

        public bool DoesProviderIdExist(Coffee coffee)
        {
            return  _providerRepository?.SingleOrDefault(p => p.Id == coffee.ProviderId) != null;
        }
    }
}