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
using dddnetcore.Domain.Contratos;
using dddnetcore.Infraestructure.Contratos;
using dddnetcore.Domain.Tarefas;
using dddnetcore.Domain.Atividades;
using dddnetcore.Infraestructure.Atividades;
using dddnetcore.Infraestructure.Tarefas;
using dddnetcore.Infraestructure.AfetacaoPerfis;
using dddnetcore.Domain.AfetacaoPerfis;
using dddnetcore.Domain.AfetacaoMensais;
using dddnetcore.Domain.Indicadores;
using dddnetcore.Domain.Perfis;
using dddnetcore.Domain.Projetos;
using dddnetcore.Infraestructure.AfetacaoMensais;
using dddnetcore.Infrastructure.Indicadores;
using dddnetcore.Infrastructure.Perfis;
using dddnetcore.Infrastructure.Projetos;
using dddnetcore.Domain.CargasMensais;
using dddnetcore.Infraestructure.CargasMensais;

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
        public DbSet<Contrato> Contratos {get; set;}
        public DbSet<Tarefa> Tarefas {get; set;}

        public DbSet<Atividade> Atividades {get; set;}
        public DbSet<AfetacaoPerfil> AfetacaoPerfis { get; set; }
        public DbSet<AfetacaoMensal> AfetacaoMensais { get; set; }
        
        public DbSet<Indicador> Indicadores {get; set;}
        public DbSet<Perfil> Perfil {get; set;}
        public DbSet<Projeto> Projetos {get; set;}
        public DbSet<CargaMensal> CargasMensais {get; set;}
        
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
            modelBuilder.ApplyConfiguration(new ContratoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AtividadeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TarefaEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AfetacaoPerfilEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AfetacaoMensalEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new IndicadorEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PerfilEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProjetoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CargaMensalEntityTypeConfiguration());


        }
    }
}