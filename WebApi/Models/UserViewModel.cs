using Entities.Entities;

namespace WebApi.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string DataNascimento { get; set; }
        public string Documento { get; set; }
        public int EscolaridadeId { get; set; }
        public string? Escolaridade { get; set; }

    }
}
