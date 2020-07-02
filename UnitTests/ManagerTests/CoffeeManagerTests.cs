using System.Collections.Generic;
using AutoMapper;
using Library.Api.Fakes;
using Library.Api.Mapper;
using Library.Core.Abstract.Managers;
using Library.Core.Abstract.Repositories;
using Library.Core.Concrete.Managers;
using Library.Core.Concrete.Models;
using Library.Core.Concrete.Repositories;
using Moq;
using NUnit.Framework;

namespace UnitTests.ManagerTests
{
    public class CoffeeManagerTests
    {
        private ICoffeeManager _manager;
        private readonly Mock<ICoffeeRepository> _mock;
     

        public CoffeeManagerTests()
        {
            _mock = new Mock<ICoffeeRepository>();
            _mock.Setup(r => r.GetAll()).Returns(new List<Coffee>());
            _mock.Setup(r => r.Get(1)).Returns(new Coffee());
            _mock.Setup(r => r.Get(2)).Returns(new Coffee());
            _mock.Setup(r => r.Add(It.IsAny<Coffee>())).Verifiable();
            _mock.Setup(r => r.Remove(It.IsAny<Coffee>())).Verifiable();
            
            _manager = new FakeCoffeeManager(_mock.Object);
            
        }

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void GetAllCoffees_WhenCalled_ReturnsListOfCoffees()
        {
            var result = _manager.GetAllCoffees();
            
            Assert.That(result, Is.InstanceOf<IEnumerable<Coffee>>());
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void GetCoffee_WhenCalled_ReturnsCoffee(int id)
        {
            var result = _manager.GetCoffee(id);
            
            Assert.That(result, Is.TypeOf<Coffee>());
        }

       
        
    }
}