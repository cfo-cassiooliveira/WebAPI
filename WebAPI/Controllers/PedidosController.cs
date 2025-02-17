using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConectionDB;
using Domain.Entidades;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly ConectionDbContext _context;

        public PedidosController(ConectionDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Pedido>>> GetPedido()
        {
            return await _context.Pedido.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(long id)
        {
            var pedido = await _context.Pedido.FindAsync(id);

            if (pedido == null)
            {
                return BadRequest("Pedido não encontrado");
            }

            return pedido;
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutPedido(long id, Pedido pedido)
        {
            var tPedido = await _context.Pedido.FindAsync(id);

            if (tPedido == null)
            {
                return BadRequest("Pedido não encontrado");
            }
            else
            {
                tPedido.IdHistorico = pedido.IdHistorico;
                tPedido.IdProduto = pedido.IdProduto;
                tPedido.QuantidadeProduto = pedido.QuantidadeProduto;
                tPedido.PedidoPor = pedido.PedidoPor;
                tPedido.IdStatusPedido = pedido.IdStatusPedido;
                tPedido.DataAlteracao = pedido.DataAlteracao;
                tPedido.AlteradoPor = pedido.AlteradoPor;
            }

                _context.Entry(tPedido).State = EntityState.Modified;

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
        public async Task<ActionResult<Pedido>> PostPedido(Pedido pedido)
        {
            _context.Pedido.Add(pedido);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPedido", new { id = pedido.IdPedido }, pedido);
        }
    }
}
