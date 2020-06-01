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
    public class ProviderController : Controller
    {
        private readonly IProviderManager _providerManager;
        private readonly IMapper _mapper;
        
        private readonly IProviderRepository _providerRepository;
        
        public ProviderController(IProviderManager providerManager, IMapper mapper, IProviderRepository providerRepository)
        {
            _providerManager = providerManager;
            _mapper = mapper;
            _providerRepository = providerRepository;
        }
        // GET
        [HttpGet]
        public IActionResult GetAllProviders()
        {
            var providers = _providerManager.GetAllProvidersWithCoffees();
            var response = _mapper.Map<IEnumerable<ProviderResponse>>(providers);

            return Ok(response);
        }
        
        //Get one
        [HttpGet("{id}")]
        public IActionResult GetProviderById([FromRoute]int id)
        {
            var provider = _providerManager.GetProviderWithCoffees(id);
            if (provider == null)
            {
                return NotFound();
            }
            var response = _mapper.Map<ProviderResponse>(provider);

            return Ok(response);
        }
        
        //Post
        [HttpPost]
        public IActionResult CreateProvider([FromBody]ProviderRequest request)
        {
            var provider = _mapper.Map<ProviderRequest, Provider>(request);
            
           _providerRepository.Add(provider);
           _providerRepository.SaveChanges();

            var response = _mapper.Map<ProviderResponse>(provider);

            return CreatedAtAction(nameof(GetProviderById), new {id = provider.Id}, response);

        }
        
        [HttpPut("{id}")]
        public IActionResult UpdateProvider([FromRoute]int id, [FromBody] ProviderRequest request)
        {
            var provider = _providerManager.FindProvider(id);
            if (provider == null)
            {
                return NotFound();
            }
            var response = _mapper.Map(request, provider);
            
            _providerRepository.SaveChanges();
            return Created("", response);
            
        }
        
        [HttpDelete("{id}")]
        public IActionResult RemoveProvider([FromRoute]int id)
        {
            var provider = _providerManager.FindProvider(id);
            if (provider == null)
            {
                return NotFound();
            }
            
            _providerRepository.Remove(provider);
            _providerRepository.SaveChanges();
            return Ok();
        }

    }
}