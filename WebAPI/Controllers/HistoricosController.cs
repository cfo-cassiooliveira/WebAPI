using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConectionDB;
using Domain.Entidades;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoricosController : ControllerBase
    {
        private readonly ConectionDbContext _context;

        public HistoricosController(ConectionDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Historico>>> GetHistorico()
        {
            return await _context.Historico.ToListAsync();
        }


        [HttpGet("{idMesa}")]
        [Authorize]
        public async Task<ActionResult<List<Historico>>> GetHistorico(int idMesa)
        {
            var historico = await _context.Historico.Where(w => w.IdMesa == idMesa && w.Ativo == true).ToListAsync(); 

            if (historico == null)
            {
                return BadRequest("Histórico não encontrado");
            }

            return historico;
        }


        [HttpPut]
        [Authorize]
        public async Task<IActionResult> PutHistorico(Historico historico)
        {
            var tHistorico = await _context.Historico.FindAsync(historico.IdHistorico);
            if (tHistorico == null)
            {
                return BadRequest("Histórico não encontrado");
            }
            else
            {
                tHistorico.DataReserva = historico.DataReserva;
                tHistorico.IdMesa = historico.IdMesa;
                tHistorico.IdUsuario = historico.IdUsuario;
                tHistorico.Ativo = historico.Ativo;
                tHistorico.DataAlteracao = DateTime.Now;
                tHistorico.AlteradoPor = historico.AlteradoPor;
            }

                _context.Entry(tHistorico).State = EntityState.Modified;

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
        public async Task<ActionResult<Historico>> PostHistorico(Historico historico)
        {
            _context.Historico.Add(historico);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHistorico", new { historico });
        }
    }
}
