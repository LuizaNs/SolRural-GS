using Microsoft.EntityFrameworkCore;
using SolRural.Models;

namespace SolRural.Data
{
    public class SolRuralDBContext(DbContextOptions<SolRuralDBContext> options) : DbContext(options)
    {
        public DbSet<Cultivo> Cultivos { get; set; }
        public DbSet<Fazenda> Fazendas { get; set; }
        public DbSet<Instalacao> Instalacoes { get; set; }
        public DbSet<Localizacao> Localizacoes { get; set; }
        public DbSet<MedicaoEnerg> MedicoesEnerg { get; set; }
        public DbSet<Proprietario> Proprietarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
