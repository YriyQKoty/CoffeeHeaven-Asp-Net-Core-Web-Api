using Library.Core.Abstract.Repositories;
using Library.Core.Concrete.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Core.Concrete.Repositories
{
    public class ProviderRepository : Repository<Provider>, IProviderRepository
    {
        public ProviderRepository(DbContext context) : base(context)
        {
        }
    }
}