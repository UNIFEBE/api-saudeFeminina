using ApiSaudeFeminina.Domain.Interfaces;
using ApiSaudeFeminina.Infrastructure.Data;
using ApiSaudeFeminina.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiSaudeFeminina.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IArtigoRepository, ArtigoRepository>();
            services.AddScoped<ICategoriaConteudoRepository, CategoriaConteudoRepository>();
            services.AddScoped<ICicloConfigRepository, CicloConfigRepository>();
            services.AddScoped<IRegistroDiarioRepository, RegistroDiarioRepository>();
            services.AddScoped<ICicloRegistroRepository, CicloRegistroRepository>();
            services.AddScoped<IConsultaRepository, ConsultaRepository>();

            return services;
        }
    }
}
