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
            CreateMap<Coffee, CoffeeResponse>().ForMember(c => c.Id, 
                opt => opt.MapFrom(m => m.Id));
            
            CreateMap<ProviderRequest,Provider>().ForMember(p => p.Id, opt => opt.Ignore());
            CreateMap<Provider, ProviderResponse>().ForMember(p => p.Id, opt => opt.MapFrom(o => o.Id));
            
            CreateMap<OriginCountryRequest,OriginCountry>().ForMember(oc => oc.Id, opt => opt.Ignore());
            CreateMap<OriginCountry, OriginCountryResponse>().ForMember(oc => oc.Id, opt => opt.MapFrom(oc => oc.Id));
        }
    }
}