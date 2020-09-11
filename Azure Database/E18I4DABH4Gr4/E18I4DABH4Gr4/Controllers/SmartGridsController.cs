using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E18I4DABH4Gr4.Models;
using E18I4DABH4Gr4.Repositories;

namespace E18I4DABH4Gr4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmartGridsController : ControllerBase
    {
        private readonly ISmartGridRepository _repo;

        public SmartGridsController(ISmartGridRepository context)
        {
            _repo = context;
        }

        // GET: api/SmartGrids
        [HttpGet]
        public IEnumerable<SmartGrid> GetSmartGrids()
        {
            return _repo.getAll();
        }

        // GET: api/SmartGrids/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSmartGrid([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var smartGrid = _repo.getById(id);

            if (smartGrid == null)
            {
                return NotFound();
            }

            return Ok(smartGrid);
        }

        // PUT: api/SmartGrids/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSmartGrid([FromRoute] int id, [FromBody] SmartGrid smartGrid)
        {
            if (SmartGridExists(id))
            {
                _repo.Update(smartGrid);
                return Ok(smartGrid);
            }

            return NoContent();
        }

        // POST: api/SmartGrids
        [HttpPost]
        public async Task<IActionResult> PostSmartGrid([FromBody] SmartGrid smartGrid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repo.Add(smartGrid);

            return CreatedAtAction("GetSmartGrid", new { id = smartGrid.SmartGridId }, smartGrid);
        }

        // DELETE: api/SmartGrids/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSmartGrid([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var smartGrid = _repo.getById(id);

            if (smartGrid == null)
            {
                return NotFound();
            }

            _repo.Delete(smartGrid);

            return Ok(smartGrid);
        }

        private bool SmartGridExists(int id)
        {
            var Exists = _repo.Find(e => e.SmartGridId == id);

            if(Exists == null)
            {
                return false;
            }
            return true;
        }
    }
}