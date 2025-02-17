using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades
{
    public class ReservaStatus
    {
        [Key]
        public int IdTipoReserva { get; set; }
        public required string StatusReserva { get; set; }
        public bool Ocupado { get; set; }
        public bool Ativo { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }
}
