using Prestamos.Core.Entidades.Configuraciones;
using Prestamos.Core.Interfaces.Configuraciones;
using Prestamos.Infraestructure.Contexto;

namespace Prestamos.Infraestructure.Repositorios.Configuraciones
{
    public class ConfiguracionRepositorio : RepositorioGenerico<Configuracion, int>, IConfiguracionRepositorio
    {
        public ConfiguracionRepositorio(PrestamoContext context) : base(context) { }
    }
}
