using System.Collections.Generic;
using System.Reflection.PortableExecutable;

namespace Library.Core.Concrete.Models
{
    public class OriginCountry
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public ICollection<Provider> Providers { get; set; }
    }
}