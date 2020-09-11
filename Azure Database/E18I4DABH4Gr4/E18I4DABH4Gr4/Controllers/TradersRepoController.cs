using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E18I4DABH4Gr4.Context;
using E18I4DABH4Gr4.Models;
using E18I4DABH4Gr4.Repositories;

namespace E18I4DABH4Gr4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradersRepoController : ControllerBase
    {
        private readonly ITraderRepository _context;

        public TradersRepoController(ITraderRepository context)
        {
            _context = context;
        }

        // GET: api/TradersRepo
        [HttpGet]
        public List<Trader> GetTraders()
        {
            return _context.GetAll();
            //return _context.GetAll().AsEnumerable();
        }

        // GET: api/TradersRepo/5
        [HttpGet("{id}")]
        public Trader GetTrader(int id)
        {
            return _context.GetById(id);
        }

        // PUT: api/TradersRepo/5
        [HttpPut("{id}")]
        public void PutTrader(int id, [FromBody] Trader trader)
        {
            _context.Update(trader);
        }

        // POST: api/TradersRepo
        [HttpPost]
        public void PostTrader([FromBody] Trader trader)
        {
            _context.Add(trader);
        }

        // DELETE: api/TradersRepo/5
        [HttpDelete("{id}")]
        public void DeleteTrader(int id)
        {
            Trader trader = _context.GetById(id);
            _context.Delete(trader);
        }

        private bool TraderExists(int id)
        {
            var Trader = _context.GetById(id);

            if (Trader == null)
            {
                return false;
            }
            return true;
        }
    }
}