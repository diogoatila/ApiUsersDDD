using Domain.Interfaces;
using Entities.Entities;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Repositories
{
    public class RepositoryHistoricoEscolar : RepositoryGenerics<HistoricoEscolar>, IHistorico
    {
        private readonly DbContextOptions<ContextBase> _OptionsBuilder;

        public RepositoryHistoricoEscolar()
        {
            _OptionsBuilder = new DbContextOptions<ContextBase>();
        }
        public async Task<HistoricoEscolar> GetEntityByUserDocument(string documento)
        {
            try
            {
                using (var dataBase = new ContextBase(_OptionsBuilder))
                {
                    return  await dataBase.HistoricoEscolar.Where(h => h.UserDocumento == documento).OrderBy(h => h.Id).LastOrDefaultAsync();
                }
            
            }
            catch (Exception ex)
            {
                var message = ex.Message + ":" + ex.InnerException;
                throw;
            }
        }
    }
}
