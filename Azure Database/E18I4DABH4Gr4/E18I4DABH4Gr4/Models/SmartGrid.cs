using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E18I4DABH4Gr4.Models
{
    public class SmartGrid
    {   
        public SmartGrid()
        {
            Consumers = new List<Prosumer>();
            Producers = new List<Prosumer>();
        }
        [Key,DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int SmartGridId { get; set; }
        public virtual List<Prosumer> Consumers { get; set; }
        public virtual List<Prosumer> Producers { get; set; }
    }
}
