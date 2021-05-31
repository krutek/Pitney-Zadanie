
using Microsoft.Extensions.Logging;
using Pitney.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pitney.Models
{
    public class LoggerHelper : ILoggerHelper
    {
        private ILogger _logger;
        public LoggerHelper(ILoggerFactory factory)
        {
            _logger = factory.CreateLogger<AddressBookController>();
        }
        public void SaveRequest(string methodName)
        {
            _logger.LogInformation("Method {MethodName} has been called on {Time}", methodName, DateTime.UtcNow);
        }
    }
}
