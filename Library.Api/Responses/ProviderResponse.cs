using Library.Core.Concrete.Models;

namespace Library.Api.Responses
{
    public class ProviderResponse
    {

        public int Id { get; set; }
      
        public string Name { get; set; }

        public int OriginCountryId { get; set; }
    }
}