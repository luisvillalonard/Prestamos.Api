using Prestamos.Core.Entidades.DataMaestra;
using Prestamos.Core.Interfaces.DataMaestra;
using Prestamos.Infraestructure.Contexto;

namespace Prestamos.Infraestructure.Repositorios.DataMaestra
{
    public class OcupacionRepositorio : RepositorioGenerico<Ocupacion, int>, IOcupacionRepositorio
    {
        public OcupacionRepositorio(PrestamoContext context) : base(context) { }
    }
}
