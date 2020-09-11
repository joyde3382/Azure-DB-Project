using E18I4DABH4Gr4.Context;
using E18I4DABH4Gr4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E18I4DABH4Gr4.Repositories
{
    public class TraderRepository : ITraderRepository
    {
        private readonly TraderContext traderContext;

        public TraderRepository(TraderContext context)
        {
            traderContext = context;
        }

        public void Add(Trader trader)
        {
            traderContext.Traders.Add(trader);
            traderContext.SaveChanges();   
        }

        public void Delete(Trader trader)
        {
            var deleteTrader = traderContext.Traders.Find(trader.Id);
            if (deleteTrader != null)
            {
                traderContext.Traders.Remove(deleteTrader);
                traderContext.SaveChanges();
            }
        }

        public List<Trader> GetAll()
        {
            return traderContext.Traders.ToList();
        }

        public Trader GetById(int id)
        {
            return traderContext.Traders.Find(id);
        }

        public void Update(Trader trader)
        {

            traderContext.Traders.Update(trader);
            traderContext.SaveChanges();

        }
    }
}
