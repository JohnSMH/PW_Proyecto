using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PW_API.Models;

namespace PW_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TorneosController : ControllerBase
    {
        private readonly TorneoappContext _context;

        public TorneosController(TorneoappContext context)
        {
            _context = context;
        }

        // GET: api/Torneos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Torneo>>> GetTorneos()
        {
          if (_context.Torneos == null)
          {
              return NotFound();
          }
            return await _context.Torneos.ToListAsync();
        }

        // GET: api/Torneos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Torneo>> GetTorneo(int id)
        {
          if (_context.Torneos == null)
          {
              return NotFound();
          }
            var torneo = await _context.Torneos.FindAsync(id);

            if (torneo == null)
            {
                return NotFound();
            }

            return torneo;
        }

        // PUT: api/Torneos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTorneo(int id, Torneo torneo)
        {
            if (id != torneo.Id)
            {
                return BadRequest();
            }

            _context.Entry(torneo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TorneoExists(id))
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

        // POST: api/Torneos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Torneo>> PostTorneo(Torneo torneo)
        {
          if (_context.Torneos == null)
          {
              return Problem("Entity set 'TorneoappContext.Torneos'  is null.");
          }
            _context.Torneos.Add(torneo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTorneo", new { id = torneo.Id }, torneo);
        }

        // DELETE: api/Torneos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTorneo(int id)
        {
            if (_context.Torneos == null)
            {
                return NotFound();
            }
            var torneo = await _context.Torneos.FindAsync(id);
            if (torneo == null)
            {
                return NotFound();
            }

            _context.Torneos.Remove(torneo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TorneoExists(int id)
        {
            return (_context.Torneos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
