using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E18I4DABH4Gr4.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace E18I4DABH4Gr4.Repositories
{
    public class SmartGridRepository : ISmartGridRepository
    {
        private readonly SmartGridDBContext context;

        public SmartGridRepository(SmartGridDBContext c)
        {
            context = c;
        }
        public List<SmartGrid> getAll()
        {
            List<SmartGrid> test = context.SmartGrids.ToList();

            foreach(var item in test)
            {
                item.Producers = context.Prosumers.Where(b => EF.Property<int>(b, "ProducerForeignKey") == item.SmartGridId).ToList();
                item.Consumers = context.Prosumers.Where(b => EF.Property<int>(b, "ConsumerForeignKey") == item.SmartGridId).ToList();
            }
            
            return test;
        }
        public SmartGrid getById(int id)
        {
            var getByIdTemp = context.SmartGrids.Find(id);

            getByIdTemp.Producers = context.Prosumers.Where(b => EF.Property<int>(b, "ProducerForeignKey") == id).ToList();
            getByIdTemp.Consumers = context.Prosumers.Where(b => EF.Property<int>(b, "ConsumerForeignKey") == id).ToList();

            return getByIdTemp;
        }
        public IEnumerable<SmartGrid> Find(Expression<Func<SmartGrid, bool>> match)
        {
            return context.SmartGrids.Where(match);
        }
        public void Add(SmartGrid s)
        {
            context.SmartGrids.Add(s);
            Save();
        }
        public void Update(SmartGrid s)
        {
            context.SmartGrids.Update(s);
            Save();
        }
        public void Delete(SmartGrid s)
        {

            var producers = context.Prosumers.Where(b => EF.Property<int>(b, "ProducerForeignKey") == s.SmartGridId).ToList();
            var consumers = context.Prosumers.Where(b => EF.Property<int>(b, "ConsumerForeignKey") == s.SmartGridId).ToList();

            context.Prosumers.RemoveRange(producers);
            context.Prosumers.RemoveRange(consumers);

            context.SmartGrids.Remove(s);
            Save();
        }
        private void Save()
        {
            context.SaveChanges();
        }
    }
}
