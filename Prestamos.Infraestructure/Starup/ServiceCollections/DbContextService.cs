using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prestamos.Infraestructure.Contexto;

namespace Pos.Infraestructure.StarupConfigurations.ServiceCollections
{
    public static class DbContextService
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PrestamoContext>(opt => {
                opt.UseSqlServer(configuration.GetConnectionString("ConnDB"));
            });

            return services;
        }
    }
}
