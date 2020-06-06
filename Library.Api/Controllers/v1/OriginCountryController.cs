using System.Collections.Generic;
using AutoMapper;
using Library.Api.Requests;
using Library.Api.Responses;
using Library.Core.Abstract.Managers;
using Library.Core.Abstract.Repositories;
using Library.Core.Concrete.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers.v1
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class OriginCountryController : Controller
    {
         private readonly IOriginCountryManager _originCountryManager;
        private readonly IMapper _mapper;
        
        
        public OriginCountryController( IOriginCountryManager originCountryManager, IMapper mapper)
        {
            _originCountryManager = originCountryManager;
            _mapper = mapper;
           
        }
        // GET
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAllCountries()
        {
            var countries = _originCountryManager.GetCountriesWithProviders();
            var response = _mapper.Map<IEnumerable<OriginCountryResponse>>(countries);

            return Ok(response);
        }
        
        //Get one
        [HttpGet("{id:min(1)}")]
        [AllowAnonymous]
        public IActionResult GetCountryById([FromRoute]int id)
        {
            var country = _originCountryManager.GetCountryWithProviders(id);
            if (country == null)
            {
                return NotFound("There is no object with such ID in a DataBase. Try another one.");
            }
            var response = _mapper.Map<OriginCountryResponse>(country);

            return Ok(response);
        }
        
        //Post
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult CreateCountry([FromBody]OriginCountryRequest request)
        {
            var country = _mapper.Map<OriginCountryRequest, OriginCountry>(request);
            
           _originCountryManager.Add(country);
           _originCountryManager.SaveChanges();

            var response = _mapper.Map<OriginCountryResponse>(country);

            return CreatedAtAction(nameof(GetCountryById), new {id = country.Id}, response);

        }
        
        [HttpPut("{id:min(1)}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult UpdateCountry([FromRoute]int id, [FromBody] OriginCountryRequest request)
        {
            var country = _originCountryManager.FindCountry(id);
            if (country == null)
            {
                return NotFound("There is no object with such ID in a DataBase. Try another one.");
            }
            var response = _mapper.Map(request, country);
            
            _originCountryManager.SaveChanges();
            return Created("", response);
            
        }
        
        [HttpDelete("{id:min(1)}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult RemoveCountry([FromRoute]int id)
        {
            var country = _originCountryManager.FindCountry(id);
            if (country == null)
            {
                return NotFound("There is no object with such ID in a DataBase. Try another one.");
            }
            
            _originCountryManager.Remove(country);
            _originCountryManager.SaveChanges();
            return Ok();
        }
    }
}