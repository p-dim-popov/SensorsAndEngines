using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SensorsAndEngines.WebApplication.Models
{
    public static class MeasurementUnitsViewModel
    {
        static MeasurementUnitsViewModel()
        {
            foreach (var measurementUnit in Utf8Json.JsonSerializer
                .Deserialize<MeasurementUnit[]>(File
                    .ReadAllText("./measurement_units.json")))
            {
                Values.Add(measurementUnit.Code, measurementUnit);
            }
        }

        public static IDictionary<string, MeasurementUnit> Values { get; set; } 
            = new Dictionary<string, MeasurementUnit>();
    }

    public class MeasurementUnit
    {
        [DataMember(Name = "code")]
        public string Code;

        [DataMember(Name = "description")]
        public string Description;

        [DataMember(Name = "name")]
        public string Name;
    }
}
