using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SensorsAndEngines.WebApplication.Hubs
{
    using Models.Home;

    public class SensorHub : Hub
    {
        public async Task<ICollection<SensorsDTO>> LoadData()
        {
            while (!SensorsDTO.Data.Any())
                await Task.Delay(25);
            return SensorsDTO.Data;
        }
    }
}
