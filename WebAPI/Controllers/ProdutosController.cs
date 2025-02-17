using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConectionDB;
using Domain.Entidades;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly ConectionDbContext _context;

        public ProdutosController(ConectionDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Produto>>> GetProduto()
        {
            return await _context.Produto.ToListAsync();
        }


        [HttpGet("{nomeProduto}")]
        [Authorize]
        public async Task<ActionResult<Produto>> GetProduto(string nomeProduto)
        {
            var produto = await _context.Produto.FindAsync(nomeProduto);

            if (produto == null)
            {
                return BadRequest("Produto não encontrado");
            }

            return produto;
        }


        [HttpPut]
        [Authorize]
        public async Task<IActionResult> PutProduto(Produto produto)
        {
            var tProduto = await _context.Produto.FindAsync(produto.IdProduto);
            if (tProduto == null)
            {
                return BadRequest("Produto não encontrado");
            }
            else
            {
                tProduto.NomeProduto = produto.NomeProduto;
                tProduto.Quantidade = produto.Quantidade;
                tProduto.ValorUnitario = produto.ValorUnitario;
                tProduto.Ativo = produto.Ativo;
                tProduto.DataAlteracao = produto.DataAlteracao;
                tProduto.AlteradoPor = produto.AlteradoPor;
            }

                _context.Entry(tProduto).State = EntityState.Modified;

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
        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
        {
            _context.Produto.Add(produto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduto", produto);
        }
    }
}
