using System.Collections.Generic;
using AutoMapper;
using Library.Api.Controllers.v1;
using Library.Api.Mapper;
using Library.Api.Requests;
using Library.Core.Abstract.Managers;
using Library.Core.Concrete.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace UnitTests.ControllerTests
{
    public class ProviderControllerTests
    {
        private Mock<IProviderManager> _mock;
        private IProviderManager _manager;
        private ProviderController _controller;

        [SetUp]
        public void Setup()
        {
            _mock = new Mock<IProviderManager>();
            _mock.Setup(m => m.GetAllProvidersWithCoffees()).Returns(new List<Provider>());
            _mock.Setup(m => m.GetProviderWithCoffees(1)).Returns(new Provider());
            _mock.Setup(m => m.GetProviderWithCoffees(2)).Returns(new Provider());
            _mock.Setup(m => m.FindProvider(1)).Returns(new Provider());
            _mock.Setup(m => m.DoesCountryIdExist(1)).Returns(true);
            _mock.Setup(m => m.RemoveProvider(1)).Verifiable();

            _manager = _mock.Object;
            
            var mapper = new Mapper(new MapperConfiguration(m => m.AddProfile<MapperConfig>()));
            
            _controller = new ProviderController(_manager, mapper);
        }


        [Test]
        public void GetAllProviders_WhenCalled_ReturnsOkObjectResult()
        {
            var result = _controller.GetAllProviders();
            
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void GetProviderById_WhenCalledWithCorrectId_ReturnsOkObjectResult(int id)
        {
            var result = _controller.GetProviderById(id);
            
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        [TestCase(0)]
        [TestCase(-2)]
        public void GetProviderById_WhenCalledWithInvalidId_ReturnsNotFoundObjectResult(int id)
        {
            var result = _controller.GetProviderById(id);
            
            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        [Test]
        public void UpdateProvider_WhenCalledWithCorrectId_ReturnsCreatedResult()
        {
            var result = _controller.UpdateProvider(1, new ProviderRequest());
            
            Assert.That(result, Is.TypeOf<CreatedResult>());
        }
        
        [Test]
        public void UpdateProvider_WhenCalledWithInvalidId_ReturnsNotFoundObjectResult()
        {
            var result = _controller.UpdateProvider(-1, new ProviderRequest());
            
            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        [Test]
        public void RemoveProvider_WhenCalledWithCorrectId_ReturnOkResult()
        {
            var result = _controller.RemoveProvider(1);
            
            Assert.That(result, Is.TypeOf<OkResult>());
        }
        
        [Test]
        public void RemoveProvider_WhenCalledWithInvalidId_ReturnNotFoundObjectResult()
        {
            var result = _controller.RemoveProvider(-1);
            
            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }

        [Test]
        public void CreateProvider_WhenCalledWithValidCountryId_ReturnsCreatedAtActionResult()
        {
            var result = _controller.CreateProvider(new ProviderRequest() {OriginCountryId = 1});
            
            Assert.That(result, Is.TypeOf<CreatedAtActionResult>());
        }
        
        [Test]
        public void CreateProvider_WhenCalledWithInvalidCountryId_ReturnsBadRequestObjectResult()
        {
            var result = _controller.CreateProvider(new ProviderRequest() {OriginCountryId = -1});
            
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

    }
}