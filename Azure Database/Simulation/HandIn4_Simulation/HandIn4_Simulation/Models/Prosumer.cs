using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandIn4_Simulation.Models
{
    public class Prosumer
    {
        public enum ProsumerType
        {
            Private, Company
        }

        [JsonProperty(PropertyName = "id")]
        public string ProsumerId { get; set; }

        public string Name { get; set; }

        public ProsumerType prosumerType { get; set; }

        public int KWhAmount { get; set; }

        // Make the Model Serializable
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
