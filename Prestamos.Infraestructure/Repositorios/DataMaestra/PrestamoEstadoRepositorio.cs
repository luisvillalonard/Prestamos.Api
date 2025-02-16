using Prestamos.Core.Entidades.DataMaestra;
using Prestamos.Core.Interfaces.DataMaestra;
using Prestamos.Infraestructure.Contexto;

namespace Prestamos.Infraestructure.Repositorios.DataMaestra
{
    public class PrestamoEstadoRepositorio :
        RepositorioGenerico<PrestamoEstado, int>, IPrestamoEstadoRepositorio
    {
        public PrestamoEstadoRepositorio(PrestamoContext context) : base(context) { }
    }
}
