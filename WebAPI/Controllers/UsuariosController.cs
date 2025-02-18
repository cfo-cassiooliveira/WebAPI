using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConectionDB;
using Domain.Entidades;
using Microsoft.AspNetCore.Authorization;
using WebAPI.Security;

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
                tUsuario.User = Key.Base64Encode(usuario.User);
                tUsuario.Senha = Key.Base64Encode(usuario.Senha);
                tUsuario.Nome = usuario.Nome;
                tUsuario.IdTipoUsuario = usuario.IdTipoUsuario;
                tUsuario.Ativo = usuario.Ativo;
                tUsuario.DataAlteracao = DateTime.Now;
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
            usuario.User = Key.Base64Encode(usuario.User);
            usuario.Senha = Key.Base64Encode(usuario.Senha);

            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { nome = usuario.Nome }, usuario);
        }
    }
}
