using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades
{
    public class UsuarioTipo
    {
        [Key]
        public int IdTipoUsuario { get; set; }
        public required string NomeTipo { get; set; }
        public bool Acesso { get; set; } = false;
        public bool Ativo { get; set; } = false;
        public DateTime? DataAlteracao { get; set; }
        public int AlteradoPor { get; set; }
    }
}
