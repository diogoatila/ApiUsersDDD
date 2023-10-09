using Domain.Interfaces;
using Domain.Interfaces.Services;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    
        public class ServiceHistorico : IServiceHistorico
        {
            private readonly IHistorico _IHistorico;

            public ServiceHistorico(IHistorico iHistorico)
            {
            _IHistorico = iHistorico;
            }

        public async Task<HistoricoEscolar> GetEntityByUserDocument(string documento)
        {
            var historico = await _IHistorico.GetEntityByUserDocument(documento);
            return historico;
        }
    }
    

}
