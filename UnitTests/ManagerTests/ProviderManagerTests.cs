using System;
using System.Collections.Generic;
using System.Linq;
using Library.Api.Fakes;
using Library.Core.Abstract.Managers;
using Library.Core.Abstract.Repositories;
using Library.Core.Concrete.Managers;
using Library.Core.Concrete.Models;
using Library.Core.Concrete.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;
using NUnit.Framework;

namespace UnitTests.ManagerTests
{
    public class ProviderManagerTests
    {
        private Mock<IProviderRepository> _mock;
        private IProviderManager _manager;
        
        
        public ProviderManagerTests()
        {
            _mock = new Mock<IProviderRepository>();
            _mock.Setup(m => m.Get(1)).Returns(It.IsAny<Provider>());
            _mock.Setup(m => m.Get(2)).Returns(It.IsAny<Provider>());
            _mock.Setup(m => m.GetProviderWithCoffees(1)).Returns(It.IsAny<Provider>());
            _mock.Setup(m => m.GetProviderWithCoffees(2)).Returns(It.IsAny<Provider>());
            _mock.Setup(m => m.GetAll()).Returns(new List<Provider>());
            _mock.Setup(m => m.GetProvidersWithCoffees()).Returns(new List<Provider>());
            
        }

        [SetUp]
        public void Setup()
        {
            _manager = new ProviderManager(_mock.Object, null);
        }

        [Test]
        public void GetAllProviders_WhenCalled_ReturnsListOfProviders()
        {
            var result = _manager.GetAllProviders();
            
            Assert.That(result, Is.InstanceOf<IEnumerable<Provider>>());
            Assert.That(result.Count(), Is.Not.Zero);
        }

        [Test]
        public void GetAllProvidersWithCoffees_WhenSuchProvidersExist_ReturnsListOfProviders()
        {
            var result = _manager.GetAllProvidersWithCoffees();
            
            Assert.That(result, Is.InstanceOf<IEnumerable<Provider>>());
            Assert.That(result.Count(), Is.Not.Zero);
        }
       

        [Test]
        public void GetProviderWithCoffees_WhenProviderExist_ReturnsProvider()
        {
            var result = _manager.GetProviderWithCoffees(1);
            
            Assert.That(result, Is.TypeOf<Provider>());
        }
        
        [Test]
        public void GetProviderWithCoffees_WhenNoProvider_ReturnsNull()
        {
            var result = _manager.GetProviderWithCoffees(2);
            
            Assert.That(result, Is.Null);
        }

        [Test]
        [TestCase(1)]
        public void FindProvider_WhenCalledWithValidId_ReturnsProvider(int id)
        {
            var result = _manager.FindProvider(id);
            
            Assert.That(result, Is.TypeOf<Provider>());
        }
        
        [Test]
        [TestCase(-1)]
        public void FindProvider_WhenCalledWithInvalidId_ReturnsNull(int id)
        {
            var result = _manager.FindProvider(id);
            
            Assert.That(result, Is.Null);
        }

        [Test]
        public void Add_WhenCalled_ShouldAddProvider()
        {
            var provider = new Provider();
            
            _manager.Add(provider);
            
            _mock.Verify(m  => m.Add(provider));
        }
    }
}