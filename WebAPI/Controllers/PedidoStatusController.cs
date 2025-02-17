using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConectionDB;
using Domain.Entidades;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoStatusController : ControllerBase
    {
        private readonly ConectionDbContext _context;

        public PedidoStatusController(ConectionDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PedidoStatus>>> GetPedidoStatus()
        {
            return await _context.PedidoStatus.ToListAsync();
        }


        [HttpGet("{nomeStatusPedido}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PedidoStatus>>> GetPedidoStatus(string nomeStatusPedido)
        {
            var pedidoStatus = await _context.PedidoStatus.Where(w => w.NomeStatusPedido.Contains(nomeStatusPedido)).ToListAsync(); 
            if (pedidoStatus == null)
            {
                return BadRequest("Status do Pedido não encontrado");
            }

            return pedidoStatus;
        }


        [HttpPut]
        [Authorize]
        public async Task<IActionResult> PutPedidoStatus(PedidoStatus pedidoStatus)
        {
            var tPedidoStatus = await _context.PedidoStatus.FindAsync(pedidoStatus.IdStatusPedido);
            if (tPedidoStatus == null)
            {
                return BadRequest("Status do Pedido não encontrado");
            }
            else
            {
                tPedidoStatus.NomeStatusPedido = pedidoStatus.NomeStatusPedido;
                tPedidoStatus.Encerrado = pedidoStatus.Encerrado;
                tPedidoStatus.DataAlteracao = pedidoStatus.DataAlteracao;
                tPedidoStatus.AlteradoPor = pedidoStatus.AlteradoPor;
            }

            _context.Entry(tPedidoStatus).State = EntityState.Modified;

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
        public async Task<ActionResult<PedidoStatus>> PostPedidoStatus(PedidoStatus pedidoStatus)
        {
            _context.PedidoStatus.Add(pedidoStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPedidoStatus", new { id = pedidoStatus.IdStatusPedido }, pedidoStatus);
        }
    }
}
