using ConectionDB;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Security;

namespace WebAPI.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ConectionDbContext _context;
        public AuthController(ConectionDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Login(string User, string Senha)
        {
            User = Key.Base64Encode(User);
            Senha = Key.Base64Encode(Senha);

            var usuario = _context.Usuario.FirstOrDefault(x => x.User == User && x.Senha == Senha);
            if (usuario == null)
            {
                return Unauthorized("Usuário ou senha inválidos");
            }
            else
            {
                var token = TokenService.GenerateToken(usuario);
                return Ok(new { user = usuario, token = token });
            }
        }
    }
}
