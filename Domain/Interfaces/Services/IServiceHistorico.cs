using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IServiceHistorico
    {
        Task<HistoricoEscolar> GetEntityByUserDocument(string documento);
    }
}
