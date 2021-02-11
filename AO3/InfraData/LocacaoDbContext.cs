using AO3.Models;
using Microsoft.EntityFrameworkCore;

namespace AO3.InfraData
{
    public class LocacaoDbContext: DbContext
    {
        public DbSet<Locacao> LocacaoVeiculo { get; set; }

        public LocacaoDbContext(DbContextOptions<LocacaoDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Locacao>().ToTable("Locacao");
        }
    }
}
