using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using SensorsAndEngines.ProtobufModels;

namespace SensorsAndEngines.WebApplication.Models.Home
{
    public class SensorsDTO
    {
        public SensorsDTO(IList<SensorCardViewModel> sensorCards, Sensors sensorsValues)
        {
            if (sensorsValues.List.Count != sensorCards.Count)
                throw new InvalidOperationException("Sensors count from config does not match actual sensors!");

            this.Time = sensorsValues.Timestamp;
            this.Models = new SensorViewModel[sensorCards.Count];
            for (int i = 0; i < sensorCards.Count; i++)
            {
                var sensorCard = sensorCards[i];
                var sensorValues = sensorsValues.List[i];
                switch (sensorsValues.List[i].TypeCase)
                {
                    case Sensor.TypeOneofCase.Analog:
                        this.Models[i] = new SensorViewModel(sensorCard, sensorValues.Analog);
                        break;
                    case Sensor.TypeOneofCase.Digital:
                        this.Models[i] = new SensorViewModel(sensorCard, sensorValues.Digital);
                        break;
                }
            }
        }

        public static ICollection<SensorsDTO> Data { get; } =
            new LinkedList<SensorsDTO>();

        [DataMember(Name = "time")]
        public uint Time { get; set; }
        [DataMember(Name = "models")]
        public SensorViewModel[] Models { get; private set; }
    }
}
