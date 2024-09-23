using Festivos.API.Models;
using Festivos.Domain.Repositorio;
using Festivos.Context.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Festivos.Context.Extensiones
{
    public static class ConfigPersistencia
    {
        public static IServiceCollection AddPersistenciaServices(this IServiceCollection services,IConfiguration configuration) 
        {
            services.AddDbContext<FestivosContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("Default"));
            });
            services.AddScoped<IFestivoRPY, FestivoRPY>();
            return services;
        }
    }
}
