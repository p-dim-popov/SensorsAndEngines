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
        private Command _command;
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
        }

        public bool IsRunning()
            => _serialPort.IsOpen;

        public void Start(ConfigViewModel config, Sensors mcuSensors)
        {
            _serialPort.PortName = config.PortName;
            _config = config;
            _command = Command.Proceed;

            try
            {
                _serialPort.Open();

                _runningCollector = Task.Factory.StartNew(ReceiveProtobuf,
                            CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Default)
                            .Unwrap();

                // Send configuration to MCU and begin
                _serialPort.Write(mcuSensors.ToByteArray(), 0, mcuSensors.CalculateSize());
            }
            catch (Exception)
            {
                _serialPort.Close();
                throw;
            }
        }

        private async Task ReceiveProtobuf()
        {
            var s = new Stopwatch(); //TODO: remove
            uint previousTimestamp = 0u;
            using var codedInputStream = new CodedInputStream(_serialPort.BaseStream);

            while (_command != Command.Stop)
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

                _serialPort.BaseStream.WriteByte((byte)_command);
            }
        }

        public async Task<bool> ResetCollector() // Not working correctly but successfully restarts the MCU
        {
            _command = Command.Stop;
            try
            {
                await _runningCollector;
            }
            catch (Exception)
            {
                return false;
            }

            _serialPort.Close();
            return true;
        }
    }
}
