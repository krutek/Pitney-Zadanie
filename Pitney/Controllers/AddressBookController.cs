using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pitney.Models;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Pitney.Controllers
{
    [Route("AddressBookController")]
    [ApiController]
    public class AddressBookController : Controller
    {
        
        private ILoggerHelper _loggerHelper;
        public AddressBookController(ILoggerHelper loggerHelper)
        {
            _loggerHelper = loggerHelper;
        }

        [HttpPost]
        [Route("AddNewAddress")]
        public bool AddNewAddress(AddressBook addressBook)
        {
            _loggerHelper.SaveRequest(MethodBase.GetCurrentMethod().Name);
            bool succes =  SaveReadToFile.SaveAddressToBook(addressBook);
            return succes;
        }
        [HttpPost]
        [Route("DeleteAddressById")]
        public bool DeleteAddressById(int id)
        {
            _loggerHelper.SaveRequest(MethodBase.GetCurrentMethod().Name);
            return SaveReadToFile.DeleteAddressByIdNumber(id);
        }
        [HttpPost]
        [Route("DeleteAllFromAddressBook")]
        public bool DeleteAll()
        {
            _loggerHelper.SaveRequest(MethodBase.GetCurrentMethod().Name);
            return SaveReadToFile.DeleteAllFromAddressBook();
        }
       
        [HttpGet]
        [Route("GetAllAddresses")]
        public List<AddressBook> GetAllAddresses()
        {
            _loggerHelper.SaveRequest(MethodBase.GetCurrentMethod().Name);
            return SaveReadToFile.ReadAddressFromBook();
        }

        [HttpGet]
        [Route("GetAddressByCity")]
        public List<AddressBook> GetAddressByCity(string city)
        {
            if(city == null)
            {
                throw new ArgumentException("You need to send city");
            }
            _loggerHelper.SaveRequest(MethodBase.GetCurrentMethod().Name);
            var Addresses = SaveReadToFile.ReadAddressFromBook().Where(x => x.City == city).ToList();
            if (Addresses.Count == 0)
            {
                throw new ArgumentException("There is no city like a " + city + " in address book");
            }
            return Addresses;
        }

        [HttpGet]
        [Route("GetLastAddress")]
        public AddressBook GetLastAddress()
        {
            _loggerHelper.SaveRequest(MethodBase.GetCurrentMethod().Name);
            var Addresses = SaveReadToFile.ReadLastAddressFromBook();
            return Addresses;
        }
    }
}
