using Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ConectionDB
{
    public class ConectionDbContext : DbContext
    {
        private IConfiguration _configuration;

        public DbSet<Config> Config { get; set; }
        public DbSet<Historico> Historico { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<PedidoStatus> PedidoStatus { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<ReservaMesa> ReservaMesa { get; set; }
        public DbSet<ReservaStatus> ReservaStatus { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<UsuarioTipo> UsuarioTipo { get; set; }

        public ConectionDbContext(IConfiguration configuration, DbContextOptions options) : base(options)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var typeDatabase = _configuration["TypeDatabase"];
            if (typeDatabase != null)
            {
                var connectionString = _configuration.GetConnectionString(typeDatabase);

                switch (typeDatabase)
                {
                    case "SQLServer":
                        optionsBuilder.UseSqlServer(connectionString);
                        break;
                    case "MySql":
                        optionsBuilder.UseMySQL(connectionString);
                        break;
                    default:
                        optionsBuilder.UseSqlServer(connectionString);
                        break;
                }
            }else
            {
                throw new ArgumentNullException("TypeDatabase is null");
            }
        }
    }
}
