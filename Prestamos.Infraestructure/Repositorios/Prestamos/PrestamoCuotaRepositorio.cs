using Prestamos.Core.Entidades.Prestamos;
using Prestamos.Core.Interfaces.Prestamos;
using Prestamos.Infraestructure.Contexto;

namespace Prestamos.Infraestructure.Repositorios.Prestamos
{
    public class PrestamoCuotaRepositorio : 
        RepositorioGenerico<PrestamoCuota, long>, IPrestamoCuotaRepositorio
    {
        public PrestamoCuotaRepositorio(PrestamoContext context) : base(context) { }
    }
}
