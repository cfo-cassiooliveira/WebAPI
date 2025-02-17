using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades
{
    public class Historico
    {
        [Key]
        public long IdHistorico { get; set; }
        public DateTime DataReserva { get; set; }
        public int IdMesa { get; set; }
        public int IdUsuario { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime? DataAlteracao { get; set; }
        public int AlteradoPor { get; set; }
    }
}
