using System.Collections.Generic;
using AutoMapper;
using Library.Api.Requests;
using Library.Api.Responses;
using Library.Core.Abstract.Managers;
using Library.Core.Abstract.Repositories;
using Library.Core.Concrete.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers.v1
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class OriginCountryController : Controller
    {
         private readonly IOriginCountryManager _originCountryManager;
        private readonly IMapper _mapper;
        
        private readonly IOriginCountryRepository _originCountryRepository;
        
        public OriginCountryController( IOriginCountryManager originCountryManager, IMapper mapper, IOriginCountryRepository originCountryRepository)
        {
            _originCountryManager = originCountryManager;
            _mapper = mapper;
            _originCountryRepository = originCountryRepository;
        }
        // GET
        [HttpGet]
        public IActionResult GetAllCountries()
        {
            var countries = _originCountryManager.GetAllCountries();
            var response = _mapper.Map<IEnumerable<OriginCountryResponse>>(countries);

            return Ok(response);
        }
        
        //Get one
        [HttpGet("{id}")]
        public IActionResult GetCountryById([FromRoute]int id)
        {
            var country = _originCountryManager.GetCountryById(id);
            if (country == null)
            {
                return NotFound();
            }
            var response = _mapper.Map<OriginCountryResponse>(country);

            return Ok(response);
        }
        
        //Post
        [HttpPost]
        public IActionResult CreateProvider([FromBody]OriginCountryRequest request)
        {
            var country = _mapper.Map<OriginCountryRequest, OriginCountry>(request);
            
           _originCountryRepository.Add(country);
           _originCountryRepository.SaveChanges();

            var response = _mapper.Map<OriginCountryResponse>(country);

            return CreatedAtAction(nameof(GetCountryById), new {id = country.Id}, response);

        }
        
        [HttpPut("{id}")]
        public IActionResult UpdateCountry([FromRoute]int id, [FromBody] OriginCountryRequest request)
        {
            var country = _originCountryManager.FindCountry(id);
            if (country == null)
            {
                return NotFound();
            }
            var response = _mapper.Map(request, country);
            
            _originCountryRepository.SaveChanges();
            return Created("", response);
            
        }
        
        [HttpDelete("{id}")]
        public IActionResult RemoveCountry([FromRoute]int id)
        {
            var country = _originCountryManager.FindCountry(id);
            if (country == null)
            {
                return NotFound();
            }
            
            _originCountryRepository.Remove(country);
            _originCountryRepository.SaveChanges();
            return Ok();
        }
    }
}