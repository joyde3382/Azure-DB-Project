using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandIn4_Simulation.Models;
using Newtonsoft.Json;

namespace HandIn4_Simulation.Models
{
    public class SmartGrid
    {
        public int SmartGridId { get; set; }
        public virtual List<Prosumer> Consumers { get; set; }
        public virtual List<Prosumer> Producers { get; set; }

        // Make the Model Serializable
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}