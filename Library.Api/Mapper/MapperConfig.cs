using System.Linq;
using AutoMapper;
using Library.Api.Requests;
using Library.Api.Responses;
using Library.Core.Concrete.Models;

namespace Library.Api.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<CoffeeRequest, Coffee>().ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<Coffee, CoffeeResponse>();
            
            CreateMap<ProviderRequest,Provider>().ForMember(p => p.Id, opt => opt.Ignore());
            CreateMap<Provider, ProviderResponse>().ForMember(p => p.Coffees, 
                opt => opt.MapFrom(p => p.Coffees.Select(c => c.Id)));
            
            CreateMap<OriginCountryRequest,OriginCountry>().ForMember(oc => oc.Id, opt => opt.Ignore());
            CreateMap<OriginCountry, OriginCountryResponse>().ForMember(ocr => ocr.Providers, 
                opt => 
                    opt.MapFrom(oc => oc.Providers.Select(p => p.Id)));
        }
    }
}