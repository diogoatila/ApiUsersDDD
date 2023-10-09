using Entities.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Configuration
{
    public class ContextBase : DbContext
    {
        public ContextBase(DbContextOptions<ContextBase> options) : base(options)
        {
        }

        public DbSet<ApplicationUser>  ApplicationUser { get; set; }
        public DbSet<HistoricoEscolar> HistoricoEscolar { get; set; }
        public DbSet<Escolaridade> Escolaridade { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ObterStringConexao());
                base.OnConfiguring(optionsBuilder);
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Seeding(modelBuilder);
        }

        private void Seeding(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ContextBase).Assembly);
        }

        public string ObterStringConexao()
        {
            return "Data Source=localhost\\SQLEXPRESS;Initial Catalog=ApiUsersDDD;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
        }
    }
}
