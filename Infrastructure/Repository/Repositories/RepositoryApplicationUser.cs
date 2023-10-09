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
    public class RepositoryApplicationUser : RepositoryGenerics<ApplicationUser>, IApplicationUser
    {
        private readonly DbContextOptions<ContextBase> _OptionsBuilder;

        public RepositoryApplicationUser()
        {
            _OptionsBuilder = new DbContextOptions<ContextBase>();
        }


    }
}
