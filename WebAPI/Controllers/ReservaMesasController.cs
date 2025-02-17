using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConectionDB;
using Domain.Entidades;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaMesasController : ControllerBase
    {
        private readonly ConectionDbContext _context;

        public ReservaMesasController(ConectionDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<ReservaMesa>>> GetReservaMesa()
        {
            return await _context.ReservaMesa.ToListAsync();
        }


        [HttpGet("{numeroMesa}")]
        [Authorize]
        public async Task<ActionResult<List<ReservaMesa>>> GetReservaMesa(int numeroMesa)
        {
            var reservaMesa = await _context.ReservaMesa.Where(w => w.NumeroMesa == numeroMesa && w.Ativo == true).ToListAsync();
            if (reservaMesa == null)
            {
                return BadRequest("Reserva não encontrada");
            }

            return reservaMesa;
        }


        [HttpPut]
        [Authorize]
        public async Task<IActionResult> PutReservaMesa(ReservaMesa reservaMesa)
        {
            var tReservaMesa = await _context.ReservaMesa.FindAsync(reservaMesa.IdMesa);
            if (tReservaMesa == null)
            {
                return BadRequest("Reserva não encontrada");
            }
            else
            {
                tReservaMesa.NumeroMesa = reservaMesa.NumeroMesa;
                tReservaMesa.QuantidadeCadeiras = reservaMesa.QuantidadeCadeiras;
                tReservaMesa.IdTipoReserva = reservaMesa.IdTipoReserva;
                tReservaMesa.Ativo = reservaMesa.Ativo;
                tReservaMesa.DataAlteracao = reservaMesa.DataAlteracao;
                tReservaMesa.AlteradoPor = reservaMesa.AlteradoPor;
            }

                _context.Entry(tReservaMesa).State = EntityState.Modified;

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
        public async Task<ActionResult<ReservaMesa>> PostReservaMesa(ReservaMesa reservaMesa)
        {
            _context.ReservaMesa.Add(reservaMesa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservaMesa", new { id = reservaMesa.IdMesa }, reservaMesa);
        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteReservaMesa(int id)
        {
            var reservaMesa = await _context.ReservaMesa.FindAsync(id);
            if (reservaMesa == null)
            {
                return BadRequest("Reserva não encontrada");
            }

            _context.ReservaMesa.Remove(reservaMesa);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
