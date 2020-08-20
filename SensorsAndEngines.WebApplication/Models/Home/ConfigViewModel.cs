namespace SensorsAndEngines.WebApplication.Models.Home
{
    using System.Runtime.Serialization;

    public class ConfigViewModel
    {
        [DataMember(Name = "sensorCards")]
        public SensorCardViewModel[] SensorCards { get; set; }
       
        [DataMember(Name = "portName")]
        public string PortName { get; set; }
    }
}
