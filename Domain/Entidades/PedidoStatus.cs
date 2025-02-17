using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades
{
    public class PedidoStatus
    {
        [Key]
        public int IdStatusPedido { get; set; }
        public required string NomeStatusPedido { get; set; }
        public bool Encerrado { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public int AlteradoPor { get; set; }
    }
}
