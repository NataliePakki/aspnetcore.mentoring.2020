using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AspNetCoreMentoring.Models;
using Serilog;
using Microsoft.AspNetCore.Diagnostics;

namespace AspNetCoreMentoring.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var error = this.HttpContext.Features.Get<IExceptionHandlerFeature>().Error;
            var requestID = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            Log.Error(error, $"Error occurs, RequestID: {requestID}");
            return View(new ErrorViewModel { RequestId = requestID });
        }
    }
}
