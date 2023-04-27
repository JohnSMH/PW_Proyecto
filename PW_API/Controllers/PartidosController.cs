using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PW_API.Models;

namespace PW_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartidosController : ControllerBase
    {
        private readonly TorneoappContext _context;

        public PartidosController(TorneoappContext context)
        {
            _context = context;
        }

        // GET: api/Partidos
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Partido>>> GetPartidos()
        {
          if (_context.Partidos == null)
          {
              return NotFound();
          }
            return await _context.Partidos
                .Include(p => p.Jugador1)
                .Include(p => p.Jugador2)
                .Include(p => p.Torneo)
                .ToListAsync();
        }

        // GET: api/Partidos/torneo/5
        [HttpGet("torneo/{id}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Partido>>> GetPartidosbyTorneo(int id)
        {
            if (_context.Partidos == null)
            {
                return NotFound();
            }
            return await _context.Partidos
                .Include(p => p.Jugador1)
                .Include(p => p.Jugador2)
                .Include(p => p.Torneo)
                .Where(p => p.TorneoId == id)
                .ToListAsync();
        }

        // GET: api/Partidos/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Partido>> GetPartido(int id)
        {
          if (_context.Partidos == null)
          {
              return NotFound();
          }
            var partido = await _context.Partidos
                .Include(p => p.Jugador1)
                .Include(p => p.Jugador2)
                .Include(p => p.Torneo)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (partido == null)
            {
                return NotFound();
            }

            return partido;
        }

        // PUT: api/Partidos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutPartido(int id, Partido partido)
        {
            if (id != partido.Id)
            {
                return BadRequest();
            }

            _context.Entry(partido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PartidoExists(id))
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

        // POST: api/Partidos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Partido>> PostPartido(Partido partido)
        {
          if (_context.Partidos == null)
          {
              return Problem("Entity set 'TorneoappContext.Partidos'  is null.");
          }
            _context.Partidos.Add(partido);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPartido", new { id = partido.Id }, partido);
        }

        // DELETE: api/Partidos/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePartido(int id)
        {
            if (_context.Partidos == null)
            {
                return NotFound();
            }
            var partido = await _context.Partidos.FindAsync(id);
            if (partido == null)
            {
                return NotFound();
            }

            _context.Partidos.Remove(partido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PartidoExists(int id)
        {
            return (_context.Partidos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
