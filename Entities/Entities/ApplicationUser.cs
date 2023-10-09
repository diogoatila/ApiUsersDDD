using Entities.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;


namespace Entities.Entities
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Documento { get; set; }
        public TipoUsuario? Tipo { get; set; }

        [ForeignKey("Escolaridade")]
        public int EscolaridadeId { get; set; }

        [NotMapped]
        public string? Escolaridade { get; set; }

    }
}
