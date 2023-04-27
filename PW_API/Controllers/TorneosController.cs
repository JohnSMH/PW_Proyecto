using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
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
        [Authorize]
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
        [Authorize]
        public async Task<ActionResult<Torneo>> GetTorneo(int id)
        {
          if (_context.Torneos == null)
          {
              return NotFound();
          }
            var torneo = await _context.Torneos.Include(t => t.Users).FirstOrDefaultAsync(t => t.Id == id);

            if (torneo == null)
            {
                return NotFound();
            }

            return torneo;
        }

        // PUT: api/Torneos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
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
        [Authorize]
        public async Task<ActionResult<Torneo>> PostTorneo(TorneoPayload torneopayload)
        {
          if (_context.Torneos == null)
          {
              return Problem("Entity set 'TorneoappContext.Torneos'  is null.");
          }
            Torneo torneo = torneopayload.Torneo;
            List<User> ListaUsuarios = _context.Users.Where(u => torneopayload.ParticipantesIDs.Contains(u.Id)).ToList();
            torneo.Users.AddRange(ListaUsuarios);

            _context.Torneos.Add(torneo);

            await _context.SaveChangesAsync();

            for (int i = 0; i < ListaUsuarios.Count - 1; i++)
            {
                for (int j = i + 1; j < ListaUsuarios.Count; j++)
                {
                    _context.Partidos.Add(new Partido
                    {
                        Jugador1Id = ListaUsuarios[i].Id,
                        Jugador2Id = ListaUsuarios[j].Id,
                        TorneoId = torneo.Id,
                        Fecha = torneo.FechaInicio
                    });

                    _context.Partidos.Add(new Partido
                    {
                        Jugador1Id = ListaUsuarios[j].Id,
                        Jugador2Id = ListaUsuarios[i].Id,
                        TorneoId = torneo.Id,
                        Fecha = torneo.FechaInicio
                    });
                }
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTorneo", new { id = torneo.Id }, torneo);
        }

        // DELETE: api/Torneos/5
        [HttpDelete("{id}")]
        [Authorize]
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
