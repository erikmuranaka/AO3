using AO3.Models;
using Microsoft.EntityFrameworkCore;

namespace AO3.InfraData
{
    public class VeiculoDbContext: DbContext
    {
        public DbSet<Veiculo> Veiculos { get; set; }

        public VeiculoDbContext(DbContextOptions<VeiculoDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Veiculo>().ToTable("Veiculos");
        }

    }
}
