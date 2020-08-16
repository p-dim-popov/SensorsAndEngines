using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SensorsAndEngines.WebApplication.Models;

namespace SensorsAndEngines.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private const string _appJson = "application/json";
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
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult GetPorts()
            => Content(Utf8Json.JsonSerializer.ToJsonString(SerialPort.GetPortNames()), _appJson);

        [HttpGet]
        public IActionResult GetMeasurementUnits()
            => Content(Utf8Json.JsonSerializer.ToJsonString(MeasurementUnitsViewModel.Values), _appJson);
    }
}