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
        private const string _appJson = "application/json";
        private readonly ILogger<HomeController> _logger;
        private readonly SerialContext _serialContext;

        public HomeController(ILogger<HomeController> logger, SerialContext serialContext)
        {
            _logger = logger;
            _serialContext = serialContext;

        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Action()
        {
            if (!_serialContext.IsRunning() && !SensorsDTO.Data.Any())
                return View(nameof(Error)/*pass error info model*/);

            return View();
        }

        [HttpPost]
        public IActionResult Action([FromBody] ConfigViewModel config)
        {
            if (_serialContext.IsRunning())
                return View(nameof(Error)/*pass error info model*/);

            if (!config.SensorCards.Any())
                return View(nameof(Error)/*pass error info model*/);

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

            try
            {

            }
            catch (Exception)
            {
                return View(nameof(Error) /*pass error info model*/);
            }
            // Initialize the serial port from cards
            _serialContext.Start(config, mcuSensorsConfig);

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

        [HttpGet]
        public IActionResult GetPorts()
            => Content(Utf8Json.JsonSerializer.ToJsonString(SerialPort.GetPortNames()), _appJson);

        [HttpGet]
        public IActionResult GetMeasurementUnits()
            => Content(Utf8Json.JsonSerializer.ToJsonString(MeasurementUnitsViewModel.Values), _appJson);

        [HttpPost]
        public async Task<IActionResult> ResetCollector()
        {
            var result = await _serialContext.ResetCollector();

            if (!result)
                return View(nameof(Error)/*pass error info model*/);

            return View(nameof(Index));
        }
    }
}