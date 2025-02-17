using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConectionDB;
using Domain.Entidades;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaStatusController : ControllerBase
    {
        private readonly ConectionDbContext _context;

        public ReservaStatusController(ConectionDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<ReservaStatus>>> GetReservaStatus()
        {
            return await _context.ReservaStatus.ToListAsync();
        }


        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ReservaStatus>> GetReservaStatus(int id)
        {
            var reservaStatus = await _context.ReservaStatus.FindAsync(id);

            if (reservaStatus == null)
            {
                return BadRequest("Status da Reserva de Mesa não encontrada");
            }

            return reservaStatus;
        }


        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutReservaStatus(int id, ReservaStatus reservaStatus)
        {
            if (id != reservaStatus.IdTipoReserva)
            {
                return BadRequest("Status da Reserva de Mesa não encontrada");
            }

            _context.Entry(reservaStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ReservaStatus>> PostReservaStatus(ReservaStatus reservaStatus)
        {
            _context.ReservaStatus.Add(reservaStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservaStatus", new { id = reservaStatus.IdTipoReserva }, reservaStatus);
        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteReservaStatus(int id)
        {
            var reservaStatus = await _context.ReservaStatus.FindAsync(id);
            if (reservaStatus == null)
            {
                return NotFound();
            }

            _context.ReservaStatus.Remove(reservaStatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
