using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using SensorsAndEngines.ProtobufModels;

namespace SensorsAndEngines.WebApplication.Models.Home
{
    public class SensorCardViewModel
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "info")]
        public string Info { get; set; }

        [DataMember(Name = "pin")]
        public int Pin { get; set; }

        [DataMember(Name = "measurementUnit")]
        public string MeasurementUnit { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "derivedData")]
        public IDictionary<string, JsonElement> DerivedData { get; set; }
            = new Dictionary<string, JsonElement>();
    }
}
