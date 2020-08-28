using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace SensorsAndEngines.WebApplication.Controllers
{
    using AutoMapper;

    using Models;
    using Models.Home;
    using ProtobufModels;
    using Hubs;

    public class HomeController : Controller
    {
        private const string AppJson = "application/json";
        private readonly ILogger<HomeController> _logger;
        private readonly SerialContext _serialContext;
        private readonly MeasurementUnitsViewModel _measurementUnits;

        public HomeController(ILogger<HomeController> logger, SerialContext serialContext, MeasurementUnitsViewModel measurementUnits)
        {
            _logger = logger;
            _serialContext = serialContext;
            _measurementUnits = measurementUnits;
        }

        public IActionResult Index()
        {
            return View(_measurementUnits);
        }

        [HttpGet]
        public IActionResult Action()
        {
            if (!_serialContext.IsRunning())
                return RedirectToAction(nameof(UserError), new UserErrorViewModel
                {
                    Name = "No sensors collection is running!",
                    RecommendedTo = "Restart the application."
                });

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Action([FromBody] ConfigViewModel config)
        {
            if (_serialContext.IsRunning())
                return RedirectToAction(nameof(UserError), new UserErrorViewModel
                {
                    Name = "Existing collection is already running"
                });

            if (!config.SensorCards.Any())
                return RedirectToAction(nameof(UserError), new UserErrorViewModel
                {
                    Name = "Cannot start collection with zero sensors",
                    RecommendedTo = "Connect some sensors and submit their cards to the application"
                });

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg
                    .CreateMap<SensorCardViewModel, Sensor>()
                    .ForMember(dest => dest.Digital,
                        opt =>
                        {
                            opt.PreCondition(src => src.Type == "digital-sensor");
                            opt.MapFrom(src => new DigitalSensor());
                        })
                    .ForMember(dest => dest.Analog,
                        opt =>
                        {
                            opt.PreCondition(src => src.Type == "analog-sensor");
                            opt.MapFrom(src => new AnalogSensor()
                            {
                                LowerRange = src.DerivedData["lowerRange"].GetInt32(),
                                UpperRange = src.DerivedData["upperRange"].GetInt32()
                            });
                        });
            });
            var mapper = mapperConfig.CreateMapper();

            SensorsDTO.Data.Clear();

            // Config sensors from the cards
            var mcuSensorsConfig = new Sensors
            {
                List = {
                    config.SensorCards
                        .Select(s => mapper
                            .Map<SensorCardViewModel, Sensor>(s))
                }
            };

            // Initialize the serial port from cards
            bool result = await _serialContext.Start(config, mcuSensorsConfig);

            if (!result)
                return RedirectToAction(nameof(UserError), new UserErrorViewModel
                {
                    Name = "Starting the collection returned false.",
                    RecommendedTo = "Talk to the developer."
                });

            return Ok();
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

        public IActionResult UserError(UserErrorViewModel userError)
        {
            return View(userError);
        }

        [HttpGet]
        public IActionResult GetPorts()
            => Content(Utf8Json.JsonSerializer.ToJsonString(SerialPort.GetPortNames()), AppJson);

        [HttpGet]
        public IActionResult GetMeasurementUnits()
            => Content(Utf8Json.JsonSerializer.ToJsonString(_measurementUnits.Values), AppJson);

        [HttpPost]
        public async Task<IActionResult> ResetCollector()
        {
            var result = await _serialContext.ResetCollector();

            if (!result)
                return RedirectToAction(nameof(UserError), new UserErrorViewModel
                {
                    Name = "Could not stop collection correctly!",
                    RecommendedTo = "Talk to the developer"
                });

            return Ok();
        }
    }
}