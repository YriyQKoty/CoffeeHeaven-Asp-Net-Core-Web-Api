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
    public class ProviderController : Controller
    {
        private readonly IProviderManager _providerManager;
        private readonly IMapper _mapper;
        
        
        public ProviderController(IProviderManager providerManager, IMapper mapper)
        {
            _providerManager = providerManager;
            _mapper = mapper;
      
        }
        // GET
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAllProviders()
        {
            var providers = _providerManager.GetAllProvidersWithCoffees();
            var response = _mapper.Map<IEnumerable<ProviderResponse>>(providers);

            return Ok(response);
        }
        
        //Get one
        [HttpGet("{id:min(1)}")]
        [AllowAnonymous]
        public IActionResult GetProviderById([FromRoute]int id)
        {
            var provider = _providerManager.GetProviderWithCoffees(id);
            if (provider == null)
            {
                return NotFound("There is no object with such ID in a DataBase. Try another one.");
            }
            var response = _mapper.Map<ProviderResponse>(provider);

            return Ok(response);
        }
        
        //Post
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult CreateProvider([FromBody]ProviderRequest request)
        {
            var provider = _mapper.Map<ProviderRequest, Provider>(request);
            
            if (!_providerManager.DoesCountryIdExist(provider))
            {
                return BadRequest("There is no such provider ID in a Database!");
            } 
            
            _providerManager.Add(provider);
            _providerManager.SaveChanges();

            var response = _mapper.Map<ProviderResponse>(provider);

            return CreatedAtAction(nameof(GetProviderById), new {id = provider.Id}, response);

        }
        
        [HttpPut("{id:min(1)}")]
        [Authorize(Roles = "Administrator, Provider")]
        public IActionResult UpdateProvider([FromRoute]int id, [FromBody] ProviderRequest request)
        {
            var provider = _providerManager.FindProvider(id);
            if (provider == null)
            {
                return NotFound("There is no object with such ID in a DataBase. Try another one.");
            }
            var response = _mapper.Map(request, provider);
            
            _providerManager.SaveChanges();
            return Created("", response);
            
        }
        
        [HttpDelete("{id:min(1)}")]
        [Authorize(Roles = "Administrator. Provider")]
        public IActionResult RemoveProvider([FromRoute]int id)
        {
            var provider = _providerManager.FindProvider(id);
            if (provider == null)
            {
                return NotFound("There is no object with such ID in a DataBase. Try another one.");
            }
            
            _providerManager.Remove(provider);
            _providerManager.SaveChanges();
            return Ok();
        }

    }
}