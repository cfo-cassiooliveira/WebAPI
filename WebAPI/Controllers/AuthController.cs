using ConectionDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Security;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ConectionDbContext _context;
        public AuthController(ConectionDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login(string User, string Senha)
        {
            var user = _context.Usuario.FirstOrDefault(x => x.User == User && x.Senha == Senha);
            if (user == null)
            {
                return Unauthorized("Usuário ou senha inválidos");
            }
            else
            {
                var token = TokenService.GenerateToken(user);
                return Ok(new { user = user, token = token });
            }
        }
    }
}
