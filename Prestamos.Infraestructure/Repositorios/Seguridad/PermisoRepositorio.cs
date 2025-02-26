using Prestamos.Core.Entidades.Seguridad;
using Prestamos.Core.Interfaces.Seguridad;
using Prestamos.Infraestructure.Contexto;

namespace Prestamos.Infraestructure.Repositorios.Seguridad
{
    public class PermisoRepositorio : RepositorioGenerico<Permiso, int>, IPermisoRepositorio
    {
        public PermisoRepositorio(PrestamoContext context) : base(context) { }
    }
}
