using System.Collections.Generic;
using AutoMapper;
using Library.Api.Controllers.v1;
using Library.Api.Mapper;
using Library.Api.Requests;
using Library.Core.Abstract.Managers;
using Library.Core.Concrete.Managers;
using Library.Core.Concrete.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace UnitTests.ControllerTests
{
    public class CoffeeControllerTests
    {
        private ICoffeeManager _manager;
        private Mock<ICoffeeManager> _mock;
        private CoffeeController _controller;
        
        
        [SetUp]
        public void Setup()
        {
            _mock = new Mock<ICoffeeManager>();
            _mock.Setup(c => c.GetCoffee(1)).Returns(new Coffee());
            _mock.Setup(c => c.GetCoffee(2)).Returns(new Coffee());
            _mock.Setup(c => c.GetAllCoffees()).Returns(new List<Coffee>());
            _mock.Setup(c => c.DoesProviderIdExist(It.IsAny<Coffee>())).Returns(It.IsAny<bool>());

            _manager = _mock.Object;
            
            var mapper = new Mapper(new MapperConfiguration(m => m.AddProfile<MapperConfig>()));
            
            _controller = new CoffeeController(_manager, mapper);
        }

        [Test]
        public void GetAllCoffees_WhenCalled_ReturnsOkResult()
        {
            var result = _controller.GetAllCoffees();
            
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }
        
        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void GetCoffee_WhenCalled_ReturnsOkResult(int id)
        {
            var result = _controller.GetCoffee(id);
            
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }
        
        [Test]
        [TestCase(100)]
        [TestCase(-1)]
        public void GetCoffee_WhenCoffeeIsNull_ReturnsNotFoundResult(int id)
        {
            var result = _controller.GetCoffee(id);
            
            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }
        
        [Test]
        [TestCase(1)]
        public void ChangeCoffee_WhenCalled_ReturnsCreatedResult(int id)
        {
            var result = _controller.ChangeCoffee(id, new CoffeeRequest() {Name = "Test Request"});
            
            Assert.That(result, Is.TypeOf<CreatedResult>());
        }
        
        
        [Test]
        [TestCase(1)]
        public void RemoveCoffee_WhenCalled_ReturnsOkObjectResult(int id)
        {
            var result = _controller.RemoveCoffee(id);
            
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }
        
        [Test]
        [TestCase(100)]
        public void RemoveCoffee_WhenObjectIsNull_ReturnsNotFoundObjectResult(int id)
        {
            var result = _controller.RemoveCoffee(id);
            
            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }
    }
}