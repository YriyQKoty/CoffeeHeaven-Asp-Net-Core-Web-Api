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
    public class CoffeeController : Controller
    {
        private readonly ICoffeeManager _coffeeManager;
        private readonly IMapper _mapper;
        
        private readonly ICoffeeRepository _coffeeRepository;
        
        public CoffeeController(ICoffeeManager coffeeManager, IMapper mapper, ICoffeeRepository coffeeRepository)
        {
            _coffeeManager = coffeeManager;
            _mapper = mapper;
            _coffeeRepository = coffeeRepository;
        }
        // GET all
        [HttpGet]
        public IActionResult GetAllCoffees()
        {
            var coffees = _coffeeManager.GetAllCoffees();
           var response = _mapper.Map<IEnumerable<CoffeeResponse>>(coffees);

           return Ok(response);
        }
        
        //Get one
        [HttpGet("{id}")]
        public IActionResult GetCoffee([FromRoute]int id)
        {
            var coffee = _coffeeManager.GetCoffee(id);
            if (coffee == null)
            {
                return NotFound();
            }
            var response = _mapper.Map<CoffeeResponse>(coffee);

            return Ok(response);
        }
        
        //Post
        [HttpPost]
        public IActionResult CreateCoffee([FromBody]CoffeeRequest request)
        {
            var coffee = _mapper.Map<CoffeeRequest, Coffee>(request);
            
             _coffeeRepository.Add(coffee);
             _coffeeRepository.SaveChanges();

             var response = _mapper.Map<CoffeeResponse>(coffee);

             return CreatedAtAction(nameof(GetCoffee), new {id = coffee.Id}, response);

        }
        
        [HttpPut("{id}")]
        public IActionResult ChangeCoffee([FromRoute]int id, [FromBody] CoffeeRequest request)
        {
            var coffee = _coffeeManager.ChangeCoffee(id);
            if (coffee == null)
            {
                return NotFound();
            }
            var response = _mapper.Map(request, coffee);
            
            _coffeeRepository.SaveChanges();
            return Created("", response);
            
        }
        
        [HttpDelete("{id}")]
        public IActionResult RemoveCoffee([FromRoute]int id)
        {
            var coffee = _coffeeManager.ChangeCoffee(id);
            if (coffee == null)
            {
                return NotFound();
            }
            
            _coffeeRepository.Remove(coffee);
            _coffeeRepository.SaveChanges();
            return Ok();
        }
        
        
    }
}