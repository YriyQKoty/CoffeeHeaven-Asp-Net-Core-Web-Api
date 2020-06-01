using System.Collections.Generic;

namespace Library.Api.Responses
{
    public class OriginCountryResponse
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public ICollection<int> Providers { get; set; }
    }
}