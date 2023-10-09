using Entities.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class HistoricoEscolar
    {
        public int Id { get; set; }
        public string Formato { get; set; }
        public string Nome { get; set; }

        public string CaminhoArquivo { get; set; }
 
        public string UserDocumento { get; set; }

    }
}

