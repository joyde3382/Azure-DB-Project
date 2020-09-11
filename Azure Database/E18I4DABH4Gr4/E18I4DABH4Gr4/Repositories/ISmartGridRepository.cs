using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E18I4DABH4Gr4.Models;
using E18I4DABH4Gr4.Controllers;
using E18I4DABH4Gr4.Context;
using System.Linq.Expressions;

namespace E18I4DABH4Gr4.Repositories
{
    public interface ISmartGridRepository
    {
        List<SmartGrid> getAll();
        SmartGrid getById(int id);
        IEnumerable<SmartGrid> Find(Expression<Func<SmartGrid, bool>> match);
        void Add(SmartGrid s);
        void Update(SmartGrid s);
        void Delete(SmartGrid s);
    }
}
