using System.Collections.Generic;
using System.Linq;
using Library.Core.Abstract.Repositories;
using Library.Core.Concrete.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Core.Concrete.Repositories
{
    public class CoffeeRepository : Repository<Coffee>, ICoffeeRepository
    {
        public CoffeeRepository(DbContext context) : base(context)
                 {
                 }
    }
}