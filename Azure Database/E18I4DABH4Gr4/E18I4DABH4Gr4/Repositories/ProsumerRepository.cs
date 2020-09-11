using E18I4DABH4Gr4.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E18I4DABH4Gr4.Repositories
{
    public class ProsumerRepository : Repository<Prosumer>, IProsumerRepository
    {
        public ProsumerRepository() : base("E18I4DABH4Gr4", "prosumerCollection")
        {

        }
        public Prosumer GetProsumer(string id)
        {
            return GetQuery().Where(x => x.ProsumerId == id).ToList().FirstOrDefault();
        }
        protected override string getId(Prosumer entity)
        {
            return entity.ProsumerId;
        }

        protected override void setId(Prosumer entity, string id)
        {
            entity.ProsumerId = id;
        }

        public IEnumerable<Prosumer> GetAllOverProducingProsumers()
        {
            return GetQuery().Where(x => x.KWhAmount > 0).ToList();
        }

        public IEnumerable<Prosumer> GetAllUnderProducingProsumers()
        {
            return GetQuery().Where(x => x.KWhAmount < 0).ToList();
        }
    }
}
