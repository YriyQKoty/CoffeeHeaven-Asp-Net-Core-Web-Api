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
    [Authorize]
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class CoffeeController : Controller
    {
        private readonly ICoffeeManager _coffeeManager;
        private readonly IMapper _mapper;
        
        
        public CoffeeController(ICoffeeManager coffeeManager, IMapper mapper)
        {
            _coffeeManager = coffeeManager;
            _mapper = mapper;
        }
        // GET all
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAllCoffees()
        {
            var coffees = _coffeeManager.GetAllCoffees();
           var response = _mapper.Map<IEnumerable<CoffeeResponse>>(coffees);

           return Ok(response);
        }
        
        [AllowAnonymous]
        [HttpGet("{id:min(1)}")]
        public IActionResult GetCoffee([FromRoute]int id)
        {
            var coffee = _coffeeManager.GetCoffee(id);
            if (coffee == null)
            {
                return NotFound("There is no object with such ID in a DataBase. Try another one.");
            }
            var response = _mapper.Map<CoffeeResponse>(coffee);

            return Ok(response);
        }
        
        //Post
        [HttpPost]
        public IActionResult CreateCoffee([FromBody]CoffeeRequest request)
        {
            var coffee = _mapper.Map<CoffeeRequest, Coffee>(request);
            
            if (!_coffeeManager.DoesProviderIdExist(coffee))
            {
                return BadRequest("There is no such provider ID in a Database!");
            }

            _coffeeManager.Add(coffee);
             _coffeeManager.SaveChanges();

             var response = _mapper.Map<CoffeeResponse>(coffee);

             return CreatedAtAction(nameof(GetCoffee), new {id = coffee.Id}, response);

        }
        
        [HttpPut("{id:min(1)}")]
        public IActionResult ChangeCoffee([FromRoute]int id, [FromBody] CoffeeRequest request)
        {
            var coffee = _coffeeManager.ChangeCoffee(id);
            
            if (coffee == null)
            {
                return NotFound("There is no object with such ID in a DataBase. Try another one.");
            }
            
            var response = _mapper.Map(request, coffee);
            
            _coffeeManager.SaveChanges();
            return Created("", response);
            
        }
        
        [HttpDelete("{id:min(1)}")]
        public IActionResult RemoveCoffee([FromRoute]int id)
        {
            var coffee = _coffeeManager.ChangeCoffee(id);
            if (coffee == null)
            {
                return NotFound("There is no object with such ID in a DataBase. Try another one.");
            }
            
            _coffeeManager.Remove(coffee);
            _coffeeManager.SaveChanges();
            return Ok();
        }
        
        
    }
}