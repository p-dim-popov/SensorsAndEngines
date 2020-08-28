using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Google.Protobuf;

namespace SensorsAndEngines.WebApplication.Models.Home
{
    public class SensorViewModel//<T> where T : IMessage // serializing generics in c# won't work so every T is now object
    {
        public SensorViewModel(SensorCardViewModel sensorCard, object sensorValues)
        {
            this.Name = sensorCard.Name;
            this.MeasurementUnit = sensorCard.MeasurementUnit;
            this.Data = sensorValues;
        }

        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "measurementUnit")]
        public string MeasurementUnit { get; set; }
        [DataMember(Name = "data")]
        public object Data { get; set; }
    }
}
