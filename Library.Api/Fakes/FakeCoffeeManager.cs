using System.Collections.Generic;
using System.Linq;
using Library.Core.Abstract.Managers;
using Library.Core.Abstract.Repositories;
using Library.Core.Concrete.Models;
using Microsoft.AspNetCore.Identity;

namespace Library.Api.Fakes
{
    public class FakeCoffeeManager : ICoffeeManager
    {
        public FakeCoffeeManager(ICoffeeRepository coffeeRepository)
        {
          
        }

        private  List<Coffee> _coffees = new List<Coffee>
        {
            new Coffee {Id = 1, Name = "Coffee1", Description = "lorem1", Price = 25, ProviderId = 1},
            new Coffee() {Id = 2, Name = "Coffee2", Description = "lorem2", Price = 50, ProviderId = 1},
            new Coffee() {Id = 3, Name = "Coffee3", Description = "lorem3", Price = 75, ProviderId = 1}
        };
        
        private List<Provider> _providers = new List<Provider>()
        {
            new Provider()
            {
                Id = 1,
            }
        };
        public int SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Coffee> GetAllCoffees()
        {
            return _coffees;
        }

        public Coffee GetCoffee(int id)
        {
            return _coffees[id];
        }
        
        
        public void Remove(Coffee coffee)
        {
            _coffees.Remove(coffee);
        }

        public void Add(Coffee coffee)
        {
           _coffees.Add(coffee);
        }
        

        public bool DoesProviderIdExist(Coffee coffee)
        {
            return  _providers.SingleOrDefault(p => p.Id == coffee.ProviderId) != null;
        }
    }
}