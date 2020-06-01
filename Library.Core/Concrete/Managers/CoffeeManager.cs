using System.Collections.Generic;
using Library.Core.Abstract.Managers;
using Library.Core.Abstract.Repositories;
using Library.Core.Concrete.Models;
using Library.Core.Concrete.Repositories;

namespace Library.Core.Concrete.Managers
{
    public class CoffeeManager : ICoffeeManager
    {
        private readonly ICoffeeRepository _coffeeRepository;

        public CoffeeManager(ICoffeeRepository coffeeRepository)
        {
            _coffeeRepository = coffeeRepository;
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
;        }
    }
}