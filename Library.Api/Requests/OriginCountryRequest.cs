using System.Collections.Generic;
using Library.Core.Concrete.Models;

namespace Library.Api.Requests
{
    public class OriginCountryRequest
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public ICollection<int> Providers { get; set; }
    }
}