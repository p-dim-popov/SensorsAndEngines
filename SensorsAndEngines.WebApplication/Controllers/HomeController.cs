using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Google.Protobuf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SensorsAndEngines.WebApplication.Controllers
{
    using AutoMapper;

    using Models;
    using Models.Home;
    using ProtobufModels;

    public class HomeController : Controller
    {
        private const string _appJson = "application/json";
        private readonly ILogger<HomeController> _logger;
        private SerialPort _serialPort;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Action()
        {
            if (!this._serialPort.IsOpen)
            {
                return View(new { Content = "Nothing is running." });
            }

            return View();
        }

        [HttpPost]
        public IActionResult Action([FromBody] SensorCardViewModel[] sensorCards)
        {
            if (!sensorCards.Any())
            {
                return View();
            }

            var config = new MapperConfiguration(cfg =>
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
            var mapper = config.CreateMapper();

            var sensors = new Sensors();
            sensors.List.AddRange(sensorCards
                .Select(s => mapper.Map<SensorCardViewModel, Sensor>(s)));

            try
            {
                this._serialPort = new SerialPort
                {
                    PortName = "COM4",
                    BaudRate = 9600,
                    Parity = Parity.None,
                    DataBits = 7,
                    StopBits = StopBits.One,
                    DtrEnable = true
                };

                //ProcessStartInfo psi = new ProcessStartInfo("cmd.exe")
                //{
                //    RedirectStandardError = true,
                //    RedirectStandardInput = true,
                //    RedirectStandardOutput = true,
                //    UseShellExecute = false
                //};
                //Process p = Process.Start(psi);
                //StreamWriter sw = p.StandardInput;

                this._serialPort.Open();

                var file = System.IO.File.CreateText("D:\\Users\\pdimp\\Pictures\\output.txt");

                this._serialPort.DataReceived +=
                    (sender, eventArgs) =>
                    {
                        var serialPort = sender as SerialPort;
                        var ss = Sensors.Parser.ParseDelimitedFrom(serialPort?.BaseStream);
                        file.WriteLine($"time: {ss.Timestamp}");
                        foreach (var s in ss.List)
                            switch (s.TypeCase)
                            {
                                case Sensor.TypeOneofCase.Analog:
                                    file.WriteLine($"analog: {s.Analog.Value} {s.MeasurementUnit}");
                                    break;
                                case Sensor.TypeOneofCase.Digital:
                                    file.WriteLine($"digital: {s.Digital.Value} {s.MeasurementUnit}, time: {s.Digital.Timestamp}");
                                    break;
                                default:
                                    file.WriteLine("Sensor type is unknown");
                                    break;
                            }
                    };

                this._serialPort.Write(sensors.ToByteArray(), 0, sensors.CalculateSize());

            }
            catch (Exception e)
            {
                _serialPort.Dispose();
                throw;
            }

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