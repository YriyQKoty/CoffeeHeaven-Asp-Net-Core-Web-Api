using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Library.Core.Concrete.Models
{
    public class Provider
    {
        
        public int Id { get; set; }
        
        public string Name { get; set; }

        public int OriginCountryId { get; set; }
        
        public OriginCountry OriginCountry{ get; set; }
        public ICollection<Coffee> Coffees { get; set; }

        
    }
}