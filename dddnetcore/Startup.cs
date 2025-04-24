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

            app.UseHttpsRedirection();
            app.UseRouting();

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

            services.AddTransient<ITipoEntregavelRepository, TipoEntregavelRepository>();
            services.AddTransient<TipoEntregavelService>();

            services.AddTransient<IEntregavelRepository, EntregavelRepository>();
            services.AddTransient<EntregavelService>();

            services.AddTransient<ITipoVinculoRepository, TipoVinculoRepository>();
            services.AddTransient<TipoVinculoService>();

        }
    }
}
