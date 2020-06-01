using Library.Core.Abstract.Repositories;
using Library.Core.Concrete.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Core.Concrete.Repositories
{
    public class OriginCountryRepository : Repository<OriginCountry>, IOriginCountryRepository
    {
        public OriginCountryRepository(DbContext context) : base(context)
        {
        }
    }
}