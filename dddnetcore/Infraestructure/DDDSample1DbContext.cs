using Microsoft.EntityFrameworkCore;
using DDDSample1.Domain.Categories;
using DDDSample1.Domain.Products;
using DDDSample1.Domain.Families;
using DDDSample1.Infrastructure.Categories;
using DDDSample1.Infrastructure.Products;
using dddnetcore.Domain.Rubricas;
using dddnetcore.Infraestructure.Rubricas;
using dddnetcore.Infraestructure.Orcamentos;
using DDDSample1.Infrastructure.Families;
using dddnetcore.Domain.Orcamentos;
using dddnetcore.Domain.Pessoas;
using dddnetcore.Domain.Contratos;

namespace DDDSample1.Infrastructure
{
    public class DDDSample1DbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Family> Families { get; set; }

        //uteis

        public DbSet<Rubrica> Rubricas {get; set;}
        public DbSet<Orcamento> Orcamentos {get; set;}

        public DbSet<Pessoa> Pessoas {get; set;}

        public DbSet<Contrato> Contratos {get; set;}
        
        public DDDSample1DbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FamilyEntityTypeConfiguration());
            // uteis
            modelBuilder.ApplyConfiguration(new RubricaEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrcamentoEntityTypeConfiguration());
        }
    }
}