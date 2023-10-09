namespace WebApi.Models
{
    public class HistoricoViewModel
    {
        public string Nome { get; set; }
        public string Documento { get; set; }
        public IFormFile CaminhoArquivo { get; set; }

    }
}
