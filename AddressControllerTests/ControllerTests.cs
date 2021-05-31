using System;
using Xunit;
using Pitney.Models;
using Pitney.Controllers;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AddressControllerTests
{
    public class ControllerTests
    {
        AddressBookController _controller;
        LoggerFactory LF = new LoggerFactory();
        private LoggerHelper _loggerHelper;

        public ControllerTests()
        {
            _loggerHelper = new LoggerHelper(LF);
            _controller = new AddressBookController(_loggerHelper);
        }

        [Theory]
        [InlineData("Katowice")]
        [InlineData("Tychy")]
        public void FeturnAddressesByCity(string city)
        {
            var createResponse = _controller.GetAddressByCity(city);
            Xunit.Assert.NotEmpty(createResponse);
        }
        [Fact]
        public void ReturnAddressesByCity_TryCatchException()
        {
            Xunit.Assert.Throws<ArgumentException>( () =>_controller.GetAddressByCity("Kozienice"));
        }
        [Fact]
        public void checkType_returnAddressesByCity()
        {
            var createResponse = _controller.GetAddressByCity("Tychy");
            Xunit.Assert.IsType<List<AddressBook>>(createResponse);
        }
        [Fact]
        public void ReturnAllAddresses()
        {
            var createResponse = _controller.GetAllAddresses();
            Xunit.Assert.IsType<List<AddressBook>>(createResponse);
        }

        [Theory]
        [InlineData(1,"Rosja","Moskwa","Rurykowicza")]
        [InlineData(2, "USA", "NewYork", "Clintona")]
        public void AddAddress(int id, string country, string city, string street)
        {
            AddressBook TestModel = new AddressBook { Id = id, Country = country, City = city, Street = street };
            var createResponse = _controller.AddNewAddress(TestModel);
            Xunit.Assert.True(createResponse);
        }
        
        [Fact]
        public void TryGetLastAddress()
        {
            var createResponse = _controller.GetLastAddress();
            Xunit.Assert.IsType<AddressBook>(createResponse);
        }

    }
}
