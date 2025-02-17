using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public required string User { get; set; }
        public required string Nome { get; set; }
        public required string Senha { get; set; }
        public int IdTipoUsuario { get; set; }
        public bool Ativo { get; set; } = false;
        public DateTime? DataAlteracao { get; set; }
    }
}
