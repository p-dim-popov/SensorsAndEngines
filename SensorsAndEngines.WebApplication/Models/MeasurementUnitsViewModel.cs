using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;

namespace SensorsAndEngines.WebApplication.Models
{
    public class MeasurementUnitsViewModel
    {
        public MeasurementUnitsViewModel(IFileProvider fileProvider)
        {
            var fileInfo = fileProvider.GetFileInfo(Path.Combine("wwwroot","measurement_units.json"));

            foreach (var measurementUnit in Utf8Json.JsonSerializer
                .Deserialize<MeasurementUnit[]>(File
                    .ReadAllText(fileInfo.PhysicalPath)))
            {
                Values.Add(measurementUnit.Code, measurementUnit);
            }
        }

        public IDictionary<string, MeasurementUnit> Values { get; set; } 
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
