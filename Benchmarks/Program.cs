using System;
using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using FastSerialization;
using Google.Protobuf;
using SensorsAndEngines.ProtobufModels;


namespace Benchmarks
{
    [MemoryDiagnoser]
    public class ProtobufBenchmark
    {
        private byte[] _bytes;
        public ProtobufBenchmark()
        {
            var sensors = new Sensors
            {
                List =
                {
                    new Sensor
                    {
                        Analog = new AnalogSensor
                        {
                            UpperRange = 1023,
                            LowerRange = 0,
                            Value = 10.1f
                        },
                        MeasurementUnit = "B47", //Kilonewton
                        Pin = 20
                    },
                    new Sensor
                    {
                        Digital = new DigitalSensor
                        {
                            Timestamp = (uint)Environment.TickCount,
                            Value = false
                        },
                        MeasurementUnit = "1N",
                        Pin = 10
                    }

                }
            };

            _bytes = sensors.ToByteArray();
        }

        [Benchmark]
        public void Deserializer() => Sensors.Parser.ParseFrom(_bytes);
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<ProtobufBenchmark>();
        }
    }
}

