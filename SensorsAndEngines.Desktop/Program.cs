using System.IO;

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
            TrySerial();

            using var webBuilder = WebApplication.Program
                .CreateHostBuilder(args)
                .Build();
            webBuilder
                .RunAsync();


            AppBuilder
                .Create()
                .UseApp<DesktopChromelyApp>()
                .Build()
                .Run(args);
        }

        private static void TrySerial(bool run = false)
        {
            if (!run)
                return;
            
            using (var serial = new SerialPort
            {
                PortName = "COM4",
                BaudRate = 9600,
                Parity = Parity.None,
                DataBits = 7,
                StopBits = StopBits.One,
                DtrEnable = true
            })
            {
                var sensors = new Sensors
                {
                    List =
                    {
                        new Sensor
                        {
                            Analog = new AnalogSensor
                            {
                                UpperRange = 100,
                                LowerRange = -200
                            },
                            MeasurementUnit = "B47", //Kilonewton
                            Pin = 20
                        }
                    }
                };

                try
                {
                    serial.Open();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return;
                }

                serial.DataReceived += (sender, eventArgs) =>
                {
                    var serialPort = sender as SerialPort;

                    var ss = Sensors.Parser.ParseDelimitedFrom(serialPort?.BaseStream);
                    Console.WriteLine(ss.Timestamp);
                    foreach (var s in ss.List)
                        Console.WriteLine($"{s.Analog.Value} {s.MeasurementUnit}");
                };

                serial.Write(sensors.ToByteArray(), 0, sensors.CalculateSize());

                Console.ReadKey();
            }
        }
    }
}
