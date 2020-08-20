using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace SensorsAndEngines.WebApplication
{
    using ProtobufModels;
    using Hubs;
    using Models.Home;

    public class SerialContext
    {
        private const byte DelayRate = 64;
        private IHubContext<SensorHub> _sensorHubContext;
        private SerialPort _serialPort;
        private ConfigViewModel _config;
        private CancellationTokenSource _tokenSource;
        private bool _shouldRun;
        private Task _runningCollector;

        public SerialContext(IHubContext<SensorHub> sensorHubContext, SerialPort serialPort)
        {
            _sensorHubContext = sensorHubContext;
            _serialPort = serialPort;
            _serialPort.BaudRate = 115200;
            _serialPort.Parity = Parity.None;
            _serialPort.DataBits = 7;
            _serialPort.StopBits = StopBits.One;
            _serialPort.DtrEnable = true;
            _tokenSource = new CancellationTokenSource();
        }

        public bool IsRunning()
            => _serialPort.IsOpen;

        public void Start(ConfigViewModel config, Sensors mcuSensors)
        {
            _serialPort.PortName = config.PortName;
            _config = config;
            _shouldRun = true;

            try
            {
                _serialPort.Open();
                var cancellationToken = _tokenSource.Token;
                // Select the delegate based on the decoding type specified
                switch (mcuSensors.Decoding)
                {
                    case Decoding.Protobuf:
                        _runningCollector = Task.Factory.StartNew(ReceiveProtobuf,
                             cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);
                        break;
                    case Decoding.Csv:
                        _runningCollector = Task.Factory.StartNew(() => ReceiveCsv(mcuSensors, config),
                              cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);
                        break;
                }

                // Send configuration to MCU and begin
                _serialPort.Write(mcuSensors.ToByteArray(), 0, mcuSensors.CalculateSize());
            }
            catch (Exception)
            {
                _serialPort.Close();
                throw;
            }
        }

        private async void ReceiveProtobuf()
        {
            var s = new Stopwatch(); //TODO: remove
            uint previousTimestamp = 0u;
            using var codedInputStream = new CodedInputStream(_serialPort.BaseStream);
            while (true)
            {
                s.Reset();
                s.Start();
                var sensorsValues = new Sensors();
                codedInputStream.ReadMessage(sensorsValues);
                var sensorsPlotViewModel =
                    new SensorsDTO(_config.SensorCards, sensorsValues);
                SensorsDTO.Data.Add(sensorsPlotViewModel);
                _sensorHubContext.Clients.All.SendAsync("ReceiveMessage", sensorsPlotViewModel);
                s.Stop();
                Trace.WriteLine($"timestamp: {sensorsValues.Timestamp - previousTimestamp}\tpc time: {s.Elapsed.TotalMilliseconds}");
                previousTimestamp = sensorsValues.Timestamp;
                await Task.Delay(DelayRate);
                if (!_shouldRun)
                    break;

                _serialPort.BaseStream.WriteByte((byte)Command.Proceed);
            }

            _tokenSource.Cancel();
        }

        private void ReceiveCsv(Sensors mcuSensors, ConfigViewModel config)
        {
            _serialPort.DataReceived +=
                async (sender, eventArgs) =>
                {
                    mcuSensors.DecodeFromCsv(_serialPort);
                    var sensorsPlotViewModel = new SensorsDTO(config.SensorCards, mcuSensors);
                    SensorsDTO.Data.Add(sensorsPlotViewModel);
                    await _sensorHubContext.Clients.All.SendAsync("ReceiveMessage", sensorsPlotViewModel);
                };
        }

        public async Task<bool> ResetCollector() // Not working correctly but successfully restarts the MCU
        {
            _shouldRun = false;
            try
            {
                await _runningCollector;
            }
            catch (Exception)
            {
                return false;
            }

            _serialPort.BaseStream.WriteByte((byte)Command.Stop);
            _serialPort.Close();
            return true;
        }
    }

    internal static class SensorsParser
    {
        internal static void DecodeFromCsv(this Sensors sensors, SerialPort stream)
        {
            var line = stream.ReadLine();
            var csv = new Queue<string>(line.Split(',', StringSplitOptions.RemoveEmptyEntries));
            sensors.Timestamp = uint.Parse(csv.Dequeue());

            foreach (var sensor in sensors.List)
            {
                switch (sensor.TypeCase)
                {
                    case Sensor.TypeOneofCase.Analog:
                        sensor.Analog.Value = float.Parse(csv.Dequeue());
                        break;
                    case Sensor.TypeOneofCase.Digital:
                        sensor.Digital.Timestamp = uint.Parse(csv.Dequeue());
                        sensor.Digital.Value = csv.Dequeue() != "0";
                        break;
                }
            }
        }

    }
}
