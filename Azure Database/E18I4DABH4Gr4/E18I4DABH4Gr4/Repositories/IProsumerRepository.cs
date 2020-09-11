using E18I4DABH4Gr4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E18I4DABH4Gr4.Repositories
{
    public interface IProsumerRepository : IRepository<Prosumer>
    {
        Prosumer GetProsumer(string id);

        IEnumerable<Prosumer> GetAllOverProducingProsumers();
        IEnumerable<Prosumer> GetAllUnderProducingProsumers();
    }
}
