using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E18I4DABH4Gr4.Context;
using E18I4DABH4Gr4.Models;

namespace E18I4DABH4Gr4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradersController : ControllerBase
    {
        private readonly TraderContext _context;

        public TradersController(TraderContext context)
        {
            _context = context;
        }

        // GET: api/Traders
        [HttpGet]
        public IEnumerable<Trader> GetTraders()
        {
            return _context.Traders;
        }

        // GET: api/Traders/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrader([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trader = await _context.Traders.FindAsync(id);

            if (trader == null)
            {
                return NotFound();
            }

            return Ok(trader);
        }

        // PUT: api/Traders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrader([FromRoute] int id, [FromBody] Trader trader)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trader.Id)
            {
                return BadRequest();
            }

            _context.Entry(trader).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TraderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Traders
        [HttpPost]
        public async Task<IActionResult> PostTrader([FromBody] Trader trader)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Traders.Add(trader);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrader", new { id = trader.Id }, trader);
        }

        // DELETE: api/Traders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrader([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trader = await _context.Traders.FindAsync(id);
            if (trader == null)
            {
                return NotFound();
            }

            _context.Traders.Remove(trader);
            await _context.SaveChangesAsync();

            return Ok(trader);
        }

        private bool TraderExists(int id)
        {
            return _context.Traders.Any(e => e.Id == id);
        }
    }
}