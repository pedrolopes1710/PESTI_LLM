using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Categories;
using DDDSample1.Infrastructure.Products;
using DDDSample1.Infrastructure.Families;
using System;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Categories;
using DDDSample1.Domain.Products;
using DDDSample1.Domain.Families;
using dddnetcore.Domain.Rubricas;
using dddnetcore.Infraestructure.Rubricas;
using dddnetcore.Domain.Orcamentos;
using dddnetcore.Infraestructure.Orcamentos;
using dddnetcore.Domain.Contratos;
using dddnetcore.Infraestructure.Contratos;
using dddnetcore.Domain.CargasMensais;
using dddnetcore.Infraestructure.CargasMensais;
using dddnetcore.Domain.TiposEntregavel;
using dddnetcore.Infraestructure.TiposEntregavel;
using dddnetcore.Domain.Entregaveis;

using dddnetcore.Domain.TiposVinculo;
using dddnetcore.Infraestructure.TiposVinculo;
using dddnetcore.Domain.Tarefas;
using DDDSample1.Domain.Atividades;
using dddnetcore.Infraestructure.Atividades;
using dddnetcore.Domain.Atividades;
using dddnetcore.Domain.Perfis;
using dddnetcore.Infrastructure.Perfis;
using dddnetcore.Domain.Despesas;
using dddnetcore.Domain.Indicadores;
using dddnetcore.Domain.Projetos;
using dddnetcore.Infraestructure.Despesas;
using dddnetcore.Infrastructure.Indicadores;
using dddnetcore.Infrastructure.Projetos;
using dddnetcore.Services;
using dddnetcore.Infraestructure.Entregaveis;
using dddnetcore.Infraestructure.Tarefas;
using dddnetcore.Domain.Pessoas;
using dddnetcore.Infraestructure.Pessoas;
using dddnetcore.Domain.AfetacaoMensais;
using dddnetcore.Infraestructure.AfetacaoPerfis;
using dddnetcore.Domain.AfetacaoPerfis;
using DDDSample1.Domain.AfetacaoPerfis;
using dddnetcore.Domain.TabelaAfetacoes;

namespace DDDSample1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<DDDSample1DbContext>(opt =>
                opt.UseSqlite(connectionString));

                // Adiciona a configuração de CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", policy =>
                {
                // Aqui, define o domínio do frontend que vai poder acessar a API
                policy.WithOrigins("http://localhost:3000")  // Porta do frontend (Vite)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });    

            ConfigureMyServices(services);

            services.AddControllers().AddNewtonsoftJson();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            
            //app.UseHttpsRedirection();
            app.UseRouting();

            // Habilita o uso do CORS
            app.UseCors("AllowSpecificOrigin");  // Nome da política que definimos antes

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureMyServices(IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<CategoryService>();

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ProductService>();

            services.AddTransient<IFamilyRepository, FamilyRepository>();
            services.AddTransient<FamilyService>();

            //* uteis

            services.AddTransient<IRubricaRepository, RubricaRepository>();
            services.AddTransient<RubricaService>();
        
            services.AddTransient<IOrcamentoRepository, OrcamentoRepository>();
            services.AddTransient<OrcamentoService>();

            services.AddTransient<IContratoRepository, ContratoRepository>();
            services.AddTransient<ContratoService>();

            services.AddTransient<ICargaMensalRepository, CargaMensalRepository>();
            services.AddTransient<CargaMensalService>();

            services.AddTransient<ITipoEntregavelRepository, TipoEntregavelRepository>();
            services.AddTransient<TipoEntregavelService>();

            services.AddTransient<IEntregavelRepository, EntregavelRepository>();
            services.AddTransient<EntregavelService>();

            services.AddTransient<ITipoVinculoRepository, TipoVinculoRepository>();
            services.AddTransient<TipoVinculoService>();

            services.AddTransient<ITarefaRepository, TarefaRepository>();
            services.AddTransient<TarefaService>(); 

            services.AddTransient<IAtividadeRepository, AtividadeRepository>();
            services.AddTransient<AtividadeService>();      
            
            services.AddTransient<IPerfilRepository, PerfilRepository>();   
            services.AddTransient<PerfilService>();

            services.AddTransient<IDespesaRepository, DespesaRepository>();
            services.AddTransient<DespesaService>();

            services.AddTransient<IPessoaRepository, PessoaRepository>();
            services.AddTransient<PessoaService>();
            
            services.AddTransient<IProjetoRepository, ProjetoRepository>();
            services.AddTransient<ProjetoService>();
           
            services.AddTransient<IIndicadorRepository, IndicadorRepository>();
            services.AddTransient<IndicadorService>();


            services.AddTransient<IAfetacaoMensalRepository, AfetacaoMensalRepository>();
            services.AddTransient<AfetacaoMensalService>();

            services.AddTransient<IAfetacaoPerfilRepository, AfetacaoPerfilRepository>();
            services.AddTransient<AfetacaoPerfilService>();

            services.AddTransient<TabelaAfetacoesService>();

        }
    }
}
