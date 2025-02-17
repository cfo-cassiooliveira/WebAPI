using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades
{
    public class ReservaMesa
    {
        [Key]
        public int IdMesa { get; set; }
        public int NumeroMesa { get; set; }
        public int? QuantidadeCadeiras { get; set; }
        public int IdTipoReserva { get; set; }
        public bool Ativo { get; set; } = false;
        public DateTime? DataAlteracao { get; set; }
        public int AlteradoPor { get; set; }
    }
}
