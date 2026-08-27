using ApiSaudeFeminina.Application.Interfaces;
using ApiSaudeFeminina.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ApiSaudeFeminina.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IArtigoService, ArtigoService>();
            services.AddScoped<ICicloService, CicloService>();
            services.AddScoped<ICicloRegistroService, CicloRegistroService>();
            return services;
        }
    }
}
