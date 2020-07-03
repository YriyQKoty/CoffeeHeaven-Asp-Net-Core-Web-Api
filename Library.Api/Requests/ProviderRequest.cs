using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Library.Core.Concrete.Models;

namespace Library.Api.Requests
{
  
    public class ProviderRequest
    {
        
        public string Name { get; set; }

        public int OriginCountryId { get; set; }
        
        public ICollection<int> Coffees { get; set; }
        
        
    }
}