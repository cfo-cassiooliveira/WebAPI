using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades
{
    public class Produto
    {
        [Key]
        public int IdProduto { get; set; }
        public required string NomeProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public bool Ativo { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public int AlteradoPor { get; set; }
    }
}
