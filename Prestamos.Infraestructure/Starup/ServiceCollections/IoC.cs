using Microsoft.Extensions.DependencyInjection;
using Prestamos.Core.Interfaces;
using Prestamos.Core.Interfaces.Clientes;
using Prestamos.Core.Interfaces.Configuraciones;
using Prestamos.Core.Interfaces.DataMaestra;
using Prestamos.Core.Interfaces.Prestamos;
using Prestamos.Core.Interfaces.Seguridad;
using Prestamos.Infraestructure.Repositorios;
using Prestamos.Infraestructure.Repositorios.Clientes;
using Prestamos.Infraestructure.Repositorios.Configuraciones;
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
            
            /* Clientes */
            services.AddTransient(typeof(IConfiguracionRepositorio), typeof(ConfiguracionRepositorio));

            /* Data Maestra */
            services.AddTransient(typeof(ICiudadRepositorio), typeof(CiudadRepositorio));
            services.AddTransient(typeof(IDocumentoTipoRepositorio), typeof(DocumentoTipoRepositorio));
            services.AddTransient(typeof(IFormaPagoRepositorio), typeof(FormaPagoRepositorio));
            services.AddTransient(typeof(IFormaPagoFechaRepositorio), typeof(FormaPagoFechaRepositorio));
            services.AddTransient(typeof(IMetodoPagoRepositorio), typeof(MetodoPagoRepositorio));
            services.AddTransient(typeof(IMonedaRepositorio), typeof(MonedaRepositorio));
            services.AddTransient(typeof(IOcupacionRepositorio), typeof(OcupacionRepositorio));
            services.AddTransient(typeof(IPrestamoEstadoRepositorio), typeof(PrestamoEstadoRepositorio));
            services.AddTransient(typeof(ISexoRepositorio), typeof(SexoRepositorio));
            services.AddTransient(typeof(IAcesorRepositorio), typeof(AcesorRepositorio));

            /* Prestamos */
            services.AddTransient(typeof(IPrestamoRepositorio), typeof(PrestamoRepositorio));
            services.AddTransient(typeof(IPrestamoCuotaRepositorio), typeof(PrestamoCuotaRepositorio));
            services.AddTransient(typeof(IPrestamoPagoRepositorio), typeof(PrestamoPagoRepositorio));

            /* Seguridad */
            services.AddTransient(typeof(IUsuarioRepositorio), typeof(UsuarioRepositorio));
            services.AddTransient(typeof(IRolRepositorio), typeof(RolRepositorio));
            services.AddTransient(typeof(IMenuRepositorio), typeof(MenuRepositorio));
            services.AddTransient(typeof(IPermisoRepositorio), typeof(PermisoRepositorio));

            return services;
        }
    }
}
