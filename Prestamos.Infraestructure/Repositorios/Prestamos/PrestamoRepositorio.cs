using Prestamos.Core.Entidades.Prestamos;
using Prestamos.Core.Interfaces.Prestamos;
using Prestamos.Infraestructure.Contexto;

namespace Prestamos.Infraestructure.Repositorios.Prestamos
{
    public class PrestamoRepositorio : RepositorioGenerico<Prestamo, long>, IPrestamoRepositorio
    {
        public PrestamoRepositorio(PrestamoContext context) : base(context) { }
    }
}
