using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades
{
    public class Pedido
    {
        [Key]
        public long IdPedido { get; set; }
        public long IdHistorico { get; set; }
        public int IdProduto { get; set; }
        public int QuantidadeProduto { get; set; }
        public int PedidoPor { get; set; }
        public int IdStatusPedido { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public int? AlteradoPor { get; set; }
    }
}
