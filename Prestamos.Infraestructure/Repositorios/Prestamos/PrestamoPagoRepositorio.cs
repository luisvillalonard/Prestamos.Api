using Prestamos.Core.Entidades.Prestamos;
using Prestamos.Core.Interfaces.Prestamos;
using Prestamos.Infraestructure.Contexto;

namespace Prestamos.Infraestructure.Repositorios.Prestamos
{
    public class PrestamoPagoRepositorio :
        RepositorioGenerico<PrestamoPago, long>, IPrestamoPagoRepositorio
    {
        public PrestamoPagoRepositorio(PrestamoContext context) : base(context) { }
    }
}
