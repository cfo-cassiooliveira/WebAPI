using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConectionDB;
using Domain.Entidades;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioTiposController : ControllerBase
    {
        private readonly ConectionDbContext _context;

        public UsuarioTiposController(ConectionDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<UsuarioTipo>>> GetUsuarioTipo()
        {
            return await _context.UsuarioTipo.ToListAsync();
        }

        [HttpGet("{nomeTipo}")]
        [Authorize]
        public async Task<ActionResult<List<UsuarioTipo>>> GetUsuarioTipo(string nomeTipo)
        {
            var usuarioTipo = await _context.UsuarioTipo.Where(w => w.NomeTipo == nomeTipo).ToListAsync();

            if (usuarioTipo == null)
            {
                return BadRequest("Tipo de Usuário não encontrado"); 
            }

            return usuarioTipo;
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> PutUsuarioTipo(UsuarioTipo usuarioTipo)
        {
            var tUsuarioTipo = await _context.UsuarioTipo.FindAsync(usuarioTipo.IdTipoUsuario);
            if (tUsuarioTipo == null)
            {
                return BadRequest("Tipo de Usuário não encontrado");
            }
            else
            {
                tUsuarioTipo.NomeTipo = usuarioTipo.NomeTipo;
                tUsuarioTipo.Acesso = usuarioTipo.Acesso;
                tUsuarioTipo.Ativo = usuarioTipo.Ativo;
                tUsuarioTipo.DataAlteracao = usuarioTipo.DataAlteracao;
                tUsuarioTipo.AlteradoPor = usuarioTipo.AlteradoPor;
            }

                _context.Entry(tUsuarioTipo).State = EntityState.Modified;

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
        public async Task<ActionResult<UsuarioTipo>> PostUsuarioTipo(UsuarioTipo usuarioTipo)
        {
            _context.UsuarioTipo.Add(usuarioTipo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuarioTipo", new { id = usuarioTipo.IdTipoUsuario }, usuarioTipo);
        }
    }
}
