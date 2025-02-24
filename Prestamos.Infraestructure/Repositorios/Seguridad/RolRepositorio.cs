using AutoMapper;
using Prestamos.Core.Entidades.Seguridad;
using Prestamos.Core.Interfaces.Seguridad;
using Prestamos.Infraestructure.Contexto;

namespace Prestamos.Infraestructure.Repositorios.Seguridad
{
    public class RolRepositorio : RepositorioGenerico<Rol, int>, IRolRepositorio
    {
        public RolRepositorio(PrestamoContext context, IMapper mapper) : base(context) { }
    }
}
