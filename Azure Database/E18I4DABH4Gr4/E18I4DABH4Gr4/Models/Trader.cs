using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E18I4DABH4Gr4.Models
{
    public class Trader
    {
        [Key]
        public int Id { get; set; }

        public string ProducerId { get; set; }

        public string ConsumerId { get; set; }

        public string KWhTransferred { get; set; }

        public DateTime TransferDate { get; set; }
    }
}
