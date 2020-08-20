using System.Diagnostics;

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
            TrySerial(true);

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

            using var serial = new SerialPort
            {
                PortName = "COM4",
                BaudRate = 115200,
                Parity = Parity.None,
                DataBits = 7,
                StopBits = StopBits.One,
                DtrEnable = true
            };

            var sensors = new Sensors
            {
                List =
                {
                    new Sensor
                    {
                        Analog = new AnalogSensor
                        {
                            UpperRange = 1023,
                            LowerRange = 0
                        },
                        MeasurementUnit = "B47", //Kilonewton
                        Pin = 20
                    },
                    new Sensor
                    {
                        Digital = new DigitalSensor(),
                        MeasurementUnit = "1N",
                        Pin = 10
                    }

                },
                Decoding = Decoding.Protobuf
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

            using var file = System.IO.File.CreateText("D:\\Users\\pdimp\\Pictures\\output.txt");
            using var codedInputStream = new CodedInputStream(serial.BaseStream);
            var s = new Stopwatch();
            serial.DataReceived += (sender, eventArgs) =>
            {
                s.Reset();
                s.Start();
                var ss = new Sensors();
                codedInputStream.ReadMessage(ss);
                s.Stop();
                Console.WriteLine($"pc time after parsing: {s.Elapsed.TotalMilliseconds} ms");
                Console.WriteLine($"timestamp: {ss.Timestamp}");
                //foreach (var s in ss.List)
                //    switch (s.TypeCase)
                //    {
                //        case Sensor.TypeOneofCase.Analog:
                //            file.WriteLine($"analog: {s.Analog.Value} {s.MeasurementUnit}");
                //            break;
                //        case Sensor.TypeOneofCase.Digital:
                //            file.WriteLine($"digital: {s.Digital.Value} {s.MeasurementUnit}, time of change: {s.Digital.Timestamp}");
                //            break;
                //        default:
                //            file.WriteLine("Sensor type is unknown");
                //            break;
                //    }
            };

            serial.Write(sensors.ToByteArray(), 0, sensors.CalculateSize());

            Console.ReadKey();
            serial.Close();
            Console.ReadKey();
        }
    }
}
