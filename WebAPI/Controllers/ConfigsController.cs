using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConectionDB;
using Domain.Entidades;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigsController : ControllerBase
    {
        private readonly ConectionDbContext _context;

        public ConfigsController(ConectionDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Config>>> GetConfigs()
        {
            return await _context.Config.ToListAsync();
        }



        [HttpPut]
        [Authorize]
        public async Task<IActionResult> PutConfig(Config config)
        {
            var tConfig = _context.Config.Find(config.IdConfig);
            if (tConfig == null)
            {
                return BadRequest("Não encontrado configuração");
            }
            else
            {
                tConfig.TotalMesas = config.TotalMesas;
                tConfig.Abertura = config.Abertura;
                tConfig.Fechamento = config.Fechamento;
                tConfig.InicioReserva = config.InicioReserva;
                tConfig.ExpiraReserva = config.ExpiraReserva;
                tConfig.DataAlteracao = DateTime.Now;
                tConfig.AlteradoPor = config.AlteradoPor;
            }

                _context.Entry(tConfig).State = EntityState.Modified;

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
        public async Task<ActionResult<Config>> PostConfig(Config config)
        {
            if (ConfigExists())
            {
                return BadRequest("Configuração já existe");
            }
            else
            {
                _context.Config.Add(config);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetConfig", config);
            }
        }


        private bool ConfigExists()
        {
            return _context.Config.Any();
        }
    }
}
