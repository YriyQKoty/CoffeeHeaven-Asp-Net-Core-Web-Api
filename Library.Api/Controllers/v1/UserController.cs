using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library.Api.Requests;
using Library.Core.Concrete.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace Library.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
      

        public UserController(IMapper mapper, UserManager<User> userManager, IConfiguration configuration, SignInManager<User> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
        }
        

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var user = _mapper.Map<User>(request);
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("User was successfully created.");
        }
        
        [HttpPost("token")]
        public async Task<IActionResult> Token([FromBody]LoginRequest request)
        {
            
            var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, false);
            if (!result.Succeeded)
            {
                return BadRequest("Login Failed.");
            }

            var appUser = _userManager.Users.SingleOrDefault(r => r.UserName == request.UserName);
            var token = GenerateJwtToken(request.UserName, appUser);

            return Ok(token);
        }
        
        private string GenerateJwtToken(string email, IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["Jwt:JwtIssuer"],
                _configuration["Jwt:JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}