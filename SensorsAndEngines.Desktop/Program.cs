namespace SensorsAndEngines.Desktop
{
    using Chromely.Core;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using WebApplication;
    using ProtobufModels;
    using System;
    using System.IO.Ports;
    using Google.Protobuf;


    class Program
    {
        static void Main(string[] args)
        {
            var analog = new Sensor
            {
                Analog = new AnalogSensor
                {
                    UpperRange = 5,
                    LowerRange = 1
                },
                MeasurementUnit = "B47" //Kilonewton
            };

            var digital = new Sensor
            {
                Digital = new DigitalSensor(),
                MeasurementUnit = "BTU" //British thermal unit
            };

            var configLine = new Sensors
            {
                List =
                {
                    analog,
                    digital
                }
            };

            var serial = new SerialPort("COM4", 9600, Parity.None, stopBits: StopBits.One, dataBits: 7)
            {
                DtrEnable = true
            };

            try
            {
                serial.Open();
                serial.DataReceived += (sender, eventArgs) =>
                {
                    var serialPort = sender as SerialPort;
                    string data = serialPort?.ReadExisting();
                    Console.Write(data);
                };

                //while (true)
                serial.Write(configLine.ToByteArray(), 0, configLine.CalculateSize());

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.ReadKey();
            //using var webBuilder = WebApplication.Program
            //    .CreateHostBuilder(args)
            //    .Build();
            //webBuilder
            //    .RunAsync();


            //AppBuilder
            //    .Create()
            //    .UseApp<DesktopChromelyApp>()
            //    .Build()
            //    .Run(args);
        }
    }
}
