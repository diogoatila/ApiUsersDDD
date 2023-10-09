using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Escolaridade 
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public virtual List<ApplicationUser> ApplicationUsers { get; set; }
    }
}
