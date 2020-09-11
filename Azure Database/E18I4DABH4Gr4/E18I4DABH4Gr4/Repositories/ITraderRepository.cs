using E18I4DABH4Gr4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E18I4DABH4Gr4.Repositories
{
    public interface ITraderRepository
    {
        List<Trader> GetAll();
        Trader GetById(int id);

        void Delete(Trader trader);
        void Add(Trader trader);
        void Update(Trader trader);
    }
}
