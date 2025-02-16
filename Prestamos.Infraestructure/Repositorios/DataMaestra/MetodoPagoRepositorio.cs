using Prestamos.Core.Entidades.DataMaestra;
using Prestamos.Core.Interfaces.DataMaestra;
using Prestamos.Infraestructure.Contexto;

namespace Prestamos.Infraestructure.Repositorios.DataMaestra
{
    public class MetodoPagoRepositorio : RepositorioGenerico<MetodoPago, int>, IMetodoPagoRepositorio
    {
        public MetodoPagoRepositorio(PrestamoContext context) : base(context) { }
    }
}
