using System.Collections.Generic;
using Library.Core.Abstract.Repositories;
using Library.Core.Concrete.Models;

namespace Library.Core.Abstract.Managers
{
    public interface ICoffeeManager : IManager
    {
       IEnumerable<Coffee> GetAllCoffees();
       
       Coffee GetCoffee(int id);
       
       Coffee ChangeCoffee (int id);
       
       Coffee RemoveCoffee (int id);
       
       void Add(Coffee coffee);

       void Remove(Coffee coffee);

       bool DoesProviderIdExist(Coffee coffee);
    }
}