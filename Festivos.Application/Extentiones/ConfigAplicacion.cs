using Festivos.Application.Servicios;
using Microsoft.Extensions.DependencyInjection;

namespace Festivos.Application.Extentiones
{
    public static class ConfigAplicacion
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection service)
        {
            service.AddScoped<FestivosService>();
            return service;
        }
    }
}
