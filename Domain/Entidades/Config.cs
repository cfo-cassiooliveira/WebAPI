using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades
{
    public class Config
    {
        [Key]
        public int IdConfig { get; set; }
        public int TotalMesas { get; set; }
        public string? Abertura { get; set; }
        public string? Fechamento { get; set; }
        public string? InicioReserva { get; set; }
        public int? ExpiraReserva { get; set; }
        public DateTime DataAlteracao { get; set; }
        public int AlteradoPor { get; set; }
    }
}
