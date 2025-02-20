using Microsoft.Extensions.DependencyInjection;
using Prestamos.Core.Interfaces;
using Prestamos.Core.Interfaces.Clientes;
using Prestamos.Core.Interfaces.DataMaestra;
using Prestamos.Core.Interfaces.Prestamos;
using Prestamos.Core.Interfaces.Seguridad;
using Prestamos.Infraestructure.Repositorios;
using Prestamos.Infraestructure.Repositorios.Clientes;
using Prestamos.Infraestructure.Repositorios.DataMaestra;
using Prestamos.Infraestructure.Repositorios.Prestamos;
using Prestamos.Infraestructure.Repositorios.Seguridad;

namespace Pos.Infraestructure.StarupConfigurations.ServiceCollections
{
    public static class IoC
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            /* Genericos */
            services.AddTransient(typeof(IRepositorioGenerico<,>), typeof(RepositorioGenerico<,>));

            /* Clientes */
            services.AddTransient(typeof(IClienteRepositorio), typeof(ClienteRepositorio));

            /* Data Maestra */
            services.AddTransient(typeof(ICiudadRepositorio), typeof(CiudadRepositorio));
            services.AddTransient(typeof(IDocumentoTipoRepositorio), typeof(DocumentoTipoRepositorio));
            services.AddTransient(typeof(IFormaPagoRepositorio), typeof(FormaPagoRepositorio));
            services.AddTransient(typeof(IMetodoPagoRepositorio), typeof(MetodoPagoRepositorio));
            services.AddTransient(typeof(IMonedaRepositorio), typeof(MonedaRepositorio));
            services.AddTransient(typeof(IOcupacionRepositorio), typeof(OcupacionRepositorio));
            services.AddTransient(typeof(IPrestamoEstadoRepositorio), typeof(PrestamoEstadoRepositorio));
            services.AddTransient(typeof(ISexoRepositorio), typeof(SexoRepositorio));
            services.AddTransient(typeof(IAcesorRepositorio), typeof(AcesorRepositorio));

            /* Prestamos */
            services.AddTransient(typeof(IPrestamoRepositorio), typeof(PrestamoRepositorio));
            services.AddTransient(typeof(IPrestamoPagoRepositorio), typeof(PrestamoPagoRepositorio));

            /* Seguridad */
            services.AddTransient(typeof(IUsuarioRepositorio), typeof(UsuarioRepositorio));

            return services;
        }
    }
}
