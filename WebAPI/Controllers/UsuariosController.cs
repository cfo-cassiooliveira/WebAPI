using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConectionDB;
using Domain.Entidades;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ConectionDbContext _context;

        public UsuariosController(ConectionDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Usuario>>> GetUsuario()
        {
            return await _context.Usuario.ToListAsync();
        }


        [HttpGet("{nome}")]
        [Authorize]
        public async Task<ActionResult<List<Usuario>>> GetUsuario(string nome)
        {
            var usuario = await _context.Usuario.Where(w => w.Nome.Contains(nome)).ToListAsync();

            if (usuario == null)
            {
                return BadRequest("Usuário não encontrado");
            }

            return usuario;
        }


        [HttpPut]
        [Authorize]
        public async Task<IActionResult> PutUsuario(Usuario usuario)
        {
            var tUsuario = await _context.Usuario.FindAsync(usuario.IdUsuario);
            if (tUsuario == null)
            {
                return BadRequest("Usuário não encontrado");
            }
            else
            {
                tUsuario.User = usuario.User;
                tUsuario.Nome = usuario.Nome;
                tUsuario.Senha = usuario.Senha;
                tUsuario.IdTipoUsuario = usuario.IdTipoUsuario;
                tUsuario.Ativo = usuario.Ativo;
                tUsuario.DataAlteracao = usuario.DataAlteracao;
            }

                _context.Entry(tUsuario).State = EntityState.Modified;

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
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.IdUsuario }, usuario);
        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
