using Prestamos.Core.Entidades.DataMaestra;
using Prestamos.Core.Interfaces.DataMaestra;
using Prestamos.Infraestructure.Contexto;

namespace Prestamos.Infraestructure.Repositorios.DataMaestra
{
    public class MonedaRepositorio : RepositorioGenerico<Moneda, int>, IMonedaRepositorio
    {
        public MonedaRepositorio(PrestamoContext context) : base(context) { }
    }
}
